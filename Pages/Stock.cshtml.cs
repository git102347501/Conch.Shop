using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Conch.Shop.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public PrivacyModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = await _daprClient.InvokeMethodAsync<IEnumerable<GetStockOutput>>(
              HttpMethod.Get,
              "ConchStock",
              "Stock");

            ViewData["StockData"] = data;
        }
    }
}