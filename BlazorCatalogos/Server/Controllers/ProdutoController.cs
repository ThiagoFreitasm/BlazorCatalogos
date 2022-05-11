using Blazor_Catalogo.Shared.Recurso;
using BlazorCatalogos.Server.Context;
using BlazorCatalogos.Server.Utils;
using BlazorCatalogos.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCatalogos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("categorias{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetProdutosCategoria(int id)
        {
            return await _context.Produtos.Where(p => p.CategoriaId == id).ToListAsync();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get([FromQuery] Paginacao paginacao, [FromQuery] string nome)
        {
            var queryable = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                queryable = queryable.Where(x => x.Nome.Contains(nome));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePorPagina);

            return await queryable.Paginar(paginacao).ToListAsync();
        }        

        [HttpGet ("{id}", Name ="GetProducto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id); 
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Put(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = new Produto { ProdutoId = id };
            _context.Remove(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }
    }
}
