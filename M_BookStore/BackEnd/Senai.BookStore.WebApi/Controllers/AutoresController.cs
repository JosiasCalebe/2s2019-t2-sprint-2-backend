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
    public class AutoresController : ControllerBase
    {
        AutorRepository autorRepository = new AutorRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            if (autorRepository.Listar().Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(autorRepository.Listar());
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(AutorDomain autor)
        {
            try
            {
                autorRepository.Cadastrar(autor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }

        }

        [HttpGet("{id}/livros")]
        public IActionResult BuscarLivros(int id)
        {
            if (autorRepository.BuscarLivros(id).Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(autorRepository.BuscarLivros(id));
            }
        }

        [HttpGet("ativos")]
        public IActionResult ListarAtivos()
        {
            if (autorRepository.ListarAtivos().Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(autorRepository.ListarAtivos());
            }
        }

        [HttpGet("nascimento")]
        public IActionResult ListarAnos90()
        {
            if (autorRepository.ListarAnos90().Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(autorRepository.ListarAnos90());
            }
        }

        [HttpGet("{ano}/nascimento")]
        public IActionResult BuscarPorAno(int ano)
        {
            if (autorRepository.BuscarPorAno(ano).Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(autorRepository.BuscarPorAno(ano));
            }
        }

        [HttpGet("{id}/ativos/livros")]
        public IActionResult BuscarLivrosAtivos(int id)
        {
            if (autorRepository.BuscarLivrosAtivos(id).Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(autorRepository.BuscarLivrosAtivos(id));
            }
        }
    }
}