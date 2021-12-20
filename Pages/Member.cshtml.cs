using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Conch.Shop.Pages
{
    public class MemberModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public MemberModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = await _daprClient.InvokeMethodAsync<IEnumerable<GetStockOutput>>(
              HttpMethod.Get,
              "ConchMember",
              "Member");

            ViewData["MemberData"] = data;
        }
    }
}