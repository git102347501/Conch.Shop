using Conch.Shared.Dtos.Order;
using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Conch.Shop.Pages
{
    public class OrderModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public OrderModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = await _daprClient.InvokeMethodAsync<IEnumerable<GetOrderOutput>>(
              HttpMethod.Get,
              "ConchOrder",
              "Order");

            ViewData["OrderData"] = data;
        }
    }
}