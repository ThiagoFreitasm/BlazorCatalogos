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
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext Context;

        public CategoriaController(AppDbContext context)
        {
            Context = context;
        }

        [HttpGet("todas")]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return await Context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get([FromQuery] Paginacao paginacao,
            [FromQuery] string nome)
        {
            //O AsQueryable() CONVERTE o IEnumerable() que é uma lista de categoria para poder passar para o method de estencao. 
            var queryable = Context.Categorias.AsQueryable();           

            if (!string.IsNullOrEmpty(nome))
            {
                queryable = queryable.Where(x => x.Nome.Contains(nome));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePorPagina);

            return await queryable.Paginar(paginacao).ToListAsync();

        }

        [HttpGet("{id}", Name="GetCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            return await Context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Categoria>>> Post(Categoria categoria)
        {
            Context.Add(categoria);
            await Context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut]
        public async Task<ActionResult<List<Categoria>>> Put(Categoria categoria)
        {
            Context.Entry(categoria).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Ok(categoria);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = new Categoria { CategoriaId = id };
            Context.Remove(categoria);
            await Context.SaveChangesAsync();
            return Ok(categoria);
        }
    }
}

