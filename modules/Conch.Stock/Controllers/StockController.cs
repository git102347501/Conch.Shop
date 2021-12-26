using Conch.Shared;
using Conch.Shared.Dtos.Stock;
using Conch.Stock.Controllers.Dtos;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conch.Stock.Controllers
{
    [Route("Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly StockDBContext _stockDBContext;
        private readonly DaprClient _daprClient;

        public StockController(ILogger<StockController> logger, StockDBContext stockDBContext, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
            _stockDBContext = stockDBContext;
           
        }

        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<GetStockOutput> GetStockList()
        {
            var stockInfo = from stock in _stockDBContext.StockMaster

                            let goods = (from goods in _stockDBContext.StockGoods
                                        where goods.StockId == stock.Id
                                        select new GetStockGoodsDto() { 
                                            GoodsId = goods.GoodsId,
                                            Name = goods.GoodsName,
                                            Num = goods.Num,
                                            FreezeNum = goods.FreezeNum,
                                            SellNum = goods.GetSellNum()
                                        }).ToList()

                            select new GetStockOutput()
                            {
                                Id = stock.Id,
                                CreateTime = stock.CreateTime,
                                Name = stock.Name,
                                UpdateTime = stock.UpdateTime,
                                GoodsList = goods.ToList()
                            };

            return stockInfo.ToList();
        }

        /// <summary>
        /// ��ȡĳ��Ʒ���ۿ��
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<int> GetStockNum(Guid id)
        {
            var gooods =  await _stockDBContext.StockGoods.Where(c=> c.GoodsId == id).FirstOrDefaultAsync();
            if (gooods == null) { 
                return -1; 
            } else { 
                return gooods.GetSellNum(); 
            }
        }

        /// <summary>
        /// UpdateStockDeductGoodsNum
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("Goods/Num")]
        public async Task<BaseResult> UpdateStockDeductGoodsNum(UpdateNumInput input)
        {
            var result = await _stockDBContext.StockGoods.Where(c => input.List.Select(d => d.Id).Contains(c.GoodsId)).ToListAsync();
            if (result == null)
            {
                return new BaseResult().IsError("商品不存在");
            }

            result.ForEach(c => c.DeductInventory(input.List.FirstOrDefault(i => i.Id == c.GoodsId).Num));

            if (_stockDBContext.SaveChanges() == result.Count)
            {
                return new BaseResult().IsSuccess("锁定成功");
            }
            else
            {
                return new BaseResult().IsError("锁定商品库存失败");
            }
        }
        
        /// <summary>
        /// UpdateStockBlockGoodsNum
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("Goods/BlockNum")]
        public async Task<BaseResult> UpdateStockBlockGoodsNum(UpdateNumInput input)
        {
            bool saveFailed;
            try
            {
                do
                {
                    saveFailed = false;
                    var result = await _stockDBContext.StockGoods.Where(c => input.List.Select(d => d.Id).Contains(c.GoodsId)).ToListAsync();
                    if (result == null || result.Count < 1)
                    {
                        return new BaseResult().IsError("商品不存在");
                    }

                    foreach (var goods in input.List)
                    {
                        // 查找商品库存
                        var results = await _stockDBContext.StockGoods.Where(c => c.GoodsId == goods.Id && (c.Num - c.FreezeNum) > 0).ToListAsync();
                        if (results == null || results.Count < 1)
                        {
                            return new BaseResult().IsError("商品库存不足");
                        }
                        // 求所有仓库该商品总库存
                        var count = results.Sum(c => c.GetSellNum());
                        if (count < goods.Num)
                        {
                            return new BaseResult().IsError("商品库存不足");
                        }
                        // 扣库存
                        foreach (var stock in results)
                        {
                            if (stock.GetSellNum() >= goods.Num)
                            {
                                stock.BlockInventory(goods.Num);
                                break;
                            }
                            else
                            {
                                var stocknum = stock.GetSellNum();
                                stock.BlockInventory(stocknum);
                                goods.Num -= stocknum;
                            }
                        }
                    }

                    try
                    {
                        if (_stockDBContext.SaveChanges() < 1)
                        {
                            return new BaseResult().IsError("锁定商品库存失败");
                        }
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        // 从数据库更新实体最新值
                        ex.Entries.Single().Reload();
                    }
                }
                while (saveFailed);
            }
            catch (Exception ex)
            {
                return new BaseResult().IsError("锁定商品库存异常："+ ex.Message);
            }
            return new BaseResult().IsSuccess("锁定成功");
        }
    }
}