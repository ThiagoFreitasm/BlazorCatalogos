using BlazorCatalogos.Server.Context;
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

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return await Context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}", Name ="GetCategoria")]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = new Categoria { CategoriaId = id };
            Context.Remove(categoria);
            await Context.SaveChangesAsync();
            return Ok(categoria);
        }
    }
}

