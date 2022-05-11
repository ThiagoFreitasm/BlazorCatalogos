using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorCatalogos.Client.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(4000);
            //Indicamos se o usuario esta autenticado e tambem seus claims
            var usuario = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(
            new ClaimsPrincipal(usuario)));
        }
    }
}
