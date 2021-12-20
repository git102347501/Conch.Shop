using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Conch.Shared;
using Conch.Shop.Dtos;
using Google.Protobuf;
using Conch.Shared.Dtos.Stock;

namespace Conch.Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public IndexModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var forecasts2 = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
              HttpMethod.Get,
              "ConchAccount",
              "WeatherForecast");
            ////ViewData["WeatherForecastData"] = forecasts;
        }

        public async Task AddOrder()
        {
            var order = await _daprClient.InvokeMethodAsync<BaseResult<OrderDtos>>(
               HttpMethod.Post,
               "ConchOrder",
               "Order/Add");

            ViewData["AddOrderData"] = order;
        }
    }
}