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
    public class GenerosController : ControllerBase
    {
        GeneroRepository generoRepository = new GeneroRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            if (generoRepository.Listar().Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(generoRepository.Listar());
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            try
            {
                generoRepository.Cadastrar(genero);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }

        }

        [HttpGet("buscar/{genero}/livros")]
        public IActionResult Buscar(string genero)
        {
            if (generoRepository.Buscar(genero).Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(generoRepository.Buscar(genero));
            }
        }

    }
}