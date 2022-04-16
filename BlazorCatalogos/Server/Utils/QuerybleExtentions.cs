using BlazorCatalogos.Shared.Recurso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCatalogos.Server.Utils
{
    public static class QuerybleExtentions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacao paginacao)
        {
            return queryable
                .Skip((paginacao.Pagina - 1) * paginacao.QuantidadePorPagina)
                .Take(paginacao.QuantidadePorPagina);
        }
    }
}
