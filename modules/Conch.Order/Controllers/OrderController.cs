using System.Text.Json.Serialization;
using Conch.Shared;
using Conch.Shared.Dtos.Order;
using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conch.Order.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly OrderDBContext _dbcontext;
        private readonly DaprClient _daprClient;

        public OrderController(ILogger<OrderController> logger, OrderDBContext dbcontext, DaprClient daprClient)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _daprClient = daprClient;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetOrderOutput>> GetOrderList()
        {
            var orderInfo = from order in _dbcontext.OrderMaster

                            let goods = (from goods in _dbcontext.OrderGoods
                                         where goods.OrderMasterId == order.Id
                                         select new OrderGoodsDto()
                                         {
                                             GoodsId = goods.GoodsId,
                                             GoodsName = goods.GoodsName,
                                             Num = goods.Num
                                         }).ToList()

                            select new GetOrderOutput()
                            {
                                Id = order.Id,
                                CreateTime = order.CreateTime,
                                Name = order.Name,
                                UpdateTime = order.UpdateTime,
                                OrderGoodsList = goods.ToList()
                            };

            return await orderInfo.ToListAsync();
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add")]
        public async Task<BaseResult<OrderMaster>> AddOrder(OrderMaster input)
        {
            // 向库存模块发起冻结库存请求
            try
            {
                var counter = await _daprClient.GetStateAsync<int>("standalone", "counter");

                //await _daprClient.SaveStateAsync("statestore", "AMS", input);
                //var weatherForecast = await _daprClient.GetStateAsync<WeatherForecast>("statestore", "AMS");
                // 冻库存
                var goods = input.OrderGoodsList.Select(c => new UpdateNumDto() { Id = c.GoodsId, Num = c.Num }).ToList();
                if (goods.Count < 1)
                {
                    throw new Exception("订单商品信息不能为空");
                }

                var stockresult = await _daprClient.InvokeMethodAsync<UpdateNumInput, BaseResult>
    (HttpMethod.Put, "ConchStock", "Stock/Goods/BlockNum", new UpdateNumInput() { List = goods });


                // 如果冻成功
                if (stockresult.Success)
                {
                    var id = Guid.NewGuid();

                    // 添加订单信息
                    var result = await _dbcontext.OrderMaster.AddAsync(new OrderMaster()
                    {
                        Id = id,
                        Name = input.Name,
                        AccountId = input.AccountId,
                        CreateTime = input.CreateTime = DateTime.Now.ToUniversalTime(),
                        OrderGoodsList = input.OrderGoodsList.Select(c => new OrderGoods()
                        {
                            GoodsId = c.GoodsId,
                            GoodsName = c.GoodsName,
                            Num = c.Num,
                            OrderMasterId = id
                        }).ToList()
                    });
      
                    if (_dbcontext.SaveChanges() > 0)
                    {                     
                        return new BaseResult<OrderMaster>().IsSuccess("创建订单成功", result.Entity);
                    }
                    else
                    {
                        return new BaseResult<OrderMaster>().IsError("创建订单失败");
                    }
                }

                return new BaseResult<OrderMaster>().IsError(stockresult.Message);
            }
            catch (InvocationException ex)
            {
                return new BaseResult<OrderMaster>().IsError("库存服务异常：" + ex.Message);
                //throw new Exception("库存服务异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<OrderMaster> UpdateOrder(OrderMaster input)
        {
            var result = await _dbcontext.OrderMaster.FindAsync(input.Id);
            if (result != null)
            {
                result.Name = input.Name;
                result.UpdateTime = DateTime.Now;
            }
            return result;
        }
    }
}
