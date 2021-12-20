﻿using Conch.Shared.Dtos.Account;
using Conch.Shared.Dtos.Stock;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Conch.Shop.Pages
{
    public class AccountModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public AccountModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = await _daprClient.InvokeMethodAsync<IEnumerable<GetAccountOutput>>(
              HttpMethod.Get,
              "ConchAccount",
              "Account");

            ViewData["AccountData"] = data;
        }
    }
}