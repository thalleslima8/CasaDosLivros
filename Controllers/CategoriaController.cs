using CasaDosLivros.Data;
using CasaDosLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CasaDosLivros.Controllers
{
    [ApiController]
    [Route("v1/categorias")]
    public class CategoriaController : Controller
    {
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();
            return categorias;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromServices] DataContext context, [FromBody] Categoria model)
        {
            if (ModelState.IsValid)
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [Route("edit/{id:int}")]
        public async Task<ActionResult<Categoria>> Post([FromServices] DataContext context, [FromBody] Categoria model, int id)
        {
            if (ModelState.IsValid)
            {
                //Recuperando Categoria do DB
                var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                //verificando se é null
                if (categoria == null)
                    return BadRequest(ModelState);

                categoria.Name = model.Name;

                //fazendo update
                context.Categorias.Update(categoria);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<ActionResult<string>> Delete([FromServices] DataContext context, int id)
        {
            if (ModelState.IsValid)
            {
                //Recuperando livro existente
                Categoria categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                if (categoria == null)
                    return BadRequest(ModelState);


                //Deletando Livro
                context.Categorias.Remove(categoria);

                await context.SaveChangesAsync();
                return $"Livro {categoria} deletado";
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
