using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCatalogos.Shared.Model
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        [MaxLength (100)]
        public string Nome { get; set; }
        [MaxLength(200)]
        public string Descricao { get; set; }
        public decimal Preco { get; set; }        
        public string ImagemUrl { get; set; }

        //Indica o relacionamento
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
