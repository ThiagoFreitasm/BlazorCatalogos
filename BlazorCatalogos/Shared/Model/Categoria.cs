using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCatalogos.Shared.Model
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required (ErrorMessage ="O nome da categoria é obrigatorio!")]
        [MaxLength (100)]
        public string Nome  { get; set; }
        [Required(ErrorMessage = "Coloque a descrição!")]
        [MaxLength (200)]
        public string Descricao  { get; set; }

        //Definir relacionamento um para muitos
        public ICollection<Produto> Produtos { get; set; }
    }
}
