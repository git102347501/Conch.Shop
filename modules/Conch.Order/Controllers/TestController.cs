using Conch.Shared;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Conch.Order.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly OrderDBContext _dbcontext;
        private readonly DaprClient _daprClient;

        public TestController(ILogger<OrderController> logger, OrderDBContext dbcontext, DaprClient daprClient)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _daprClient = daprClient;
        }
        
        [HttpGet]
        public async Task<BaseResult<int>> GetTestAsync()
        {
            // 向库存模块发起冻结库存请求
            try
            {
                var daprClient = new DaprClientBuilder().Build();
                var counter = await daprClient.GetStateAsync<int>("statestore", "counter");
                //var counter = await _daprClient.GetStateAsync<int>("standalone", "counter");
                if (counter < 99)
                {
                    counter++;
                    await daprClient.SaveStateAsync("statestore","counter",counter);
                }
                return new BaseResult<int>(counter);
            }
            catch (Exception exception)
            {
                return new BaseResult<int>().IsError(exception.InnerException.Message);
            }
        }
    }
}