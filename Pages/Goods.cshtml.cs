using Conch.Shared.Dtos.Goods;
using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Conch.Shop.Pages
{
    public class GoodsModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public GoodsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = await _daprClient.InvokeMethodAsync<IEnumerable<GetGoodsOutput>>(
              HttpMethod.Get,
              "ConchGoods",
              "Goods");

            ViewData["GoodsData"] = data;
        }
    }
}