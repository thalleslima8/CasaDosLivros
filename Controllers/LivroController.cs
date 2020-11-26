using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CasaDosLivros.Models;
using CasaDosLivros.Data;

namespace CasaDosLivros.Controllers
{
    [ApiController]
    [Route("v1/livros")]
    public class LivroController : Controller
    {   
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Livro>>> Get([FromServices] DataContext context)
        {
            var livros = await context.Livros.Include(x => x.Categoria).ToListAsync();
            return livros;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Livro>> GetById([FromServices] DataContext context, int id)
        {
            var livro = await context.Livros.Include(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return livro;
        }

        [HttpGet]
        [Route("categorias/{id:int}")]
        public async Task<ActionResult<List<Livro>>> GetByCategoria([FromServices] DataContext context, int id)
        {
            var livros = await context.Livros
                                .Include(x => x.Categoria)
                                .AsNoTracking()
                                .Where(x => x.CategoriaId == id)
                                .ToListAsync();
            return livros;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Livro>> Post([FromServices] DataContext context, [FromBody] Livro model)
        {
            if (ModelState.IsValid)
            {
                //Salvando Livro
                context.Livros.Add(model);

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
        public async Task<ActionResult<Livro>> Put([FromServices] DataContext context, [FromBody] Livro model, int id)
        {
            if (ModelState.IsValid)
            {
                //Recuperando livro existente
                Livro livro = await context.Livros.FirstOrDefaultAsync(x => x.Id == id);

                if (livro == null)
                    return BadRequest(ModelState);

                livro.Title = model.Title;
                livro.Author = model.Author;
                livro.Price = model.Price;
                livro.CategoriaId = model.CategoriaId;

                context.Livros.Update(livro);

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
                Livro livro = await context.Livros.FirstOrDefaultAsync(x => x.Id == id);

                if (livro == null)
                    return BadRequest(ModelState);

                
                //Deletando Livro
                context.Livros.Remove(livro);

                await context.SaveChangesAsync();
                return $"Livro {livro} deletado";
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


    }
}
