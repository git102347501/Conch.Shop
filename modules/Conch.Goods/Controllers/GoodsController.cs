using Conch.Shared;
using Conch.Shared.Dtos.Goods;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conch.Goods.Controllers
{
    [ApiController]
    [Route("Goods")]
    public class GoodsController : ControllerBase
    {
        private readonly ILogger<GoodsController> _logger;
        private readonly GoodsDBContext _goodsDBContext;
        private readonly DaprClient _daprClient;

        public GoodsController(ILogger<GoodsController> logger, GoodsDBContext dbcontext, DaprClient daprClient)
        {
            _logger = logger;
            _goodsDBContext = dbcontext;
            _daprClient = daprClient;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetGoodsOutput>> GetGoodsList()
        {
            return await _goodsDBContext.GoodsMaster.Select(c=> new GetGoodsOutput() { 
                Id = c.Id,
                Name = c.Name,
                CreateTime = c.CreateTime,
                UpdateTime = c.UpdateTime
            }).ToListAsync();
        }
    }
}