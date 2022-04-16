using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCatalogos.Server.Utils
{
    //Por ser um método de extencao a classe tem que ser static
    public static class HttpContextExtentions
    {
        //InserirParametroEmPageResponse = É UM METODO DE EXTENCAO DO .NET
        public async static Task InserirParametroEmPageResponse<T>(this HttpContext context,
           IQueryable<T> queryable, int quantidadeTotalRegistrosAExibir)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double quantidadeRegistrosTotal = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(quantidadeRegistrosTotal / quantidadeTotalRegistrosAExibir);

            //salvando as informações no header do response
            context.Response.Headers.Add("quantidadeRegistrosTotal", quantidadeRegistrosTotal.ToString());
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
