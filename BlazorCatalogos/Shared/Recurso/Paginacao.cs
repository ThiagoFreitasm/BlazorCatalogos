using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCatalogos.Shared.Recurso
{
    public class Paginacao
    {
        public int Pagina { get; set; } = 1;
        public int QuantidadePorPagina { get; set; } = 5;
    }
}
