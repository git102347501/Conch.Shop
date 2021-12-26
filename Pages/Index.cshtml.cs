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
        public IndexModel()
        {
        }

        public async Task OnGet()
        {
        }
        
    }
}