using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.BookStore.WebApi.Domains;
using Senai.BookStore.WebApi.Repositories;

namespace Senai.BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        LivroRepository livroRepository = new LivroRepository();
        [HttpGet]
        public IActionResult Listar()
        {
            if (livroRepository.Listar().Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(livroRepository.Listar());
            }
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {
                if (livroRepository.Buscar(id) == null)
                {
                    return NotFound();
                }
                return Ok(livroRepository.Buscar(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult Cadastrar(LivroDomain livro)
        {
            try
            {
                livroRepository.Cadastrar(livro);
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (livroRepository.Buscar(id) == null)
                {
                    return NotFound();
                }
                livroRepository.Deletar(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(LivroDomain livro, int id)
        {
            if (livroRepository.Buscar(id) == null)
            {
                return NotFound();
            }
            try
            {
                livroRepository.Alterar(livro, id);
                return Ok(livro);
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
            
        }

    }
}