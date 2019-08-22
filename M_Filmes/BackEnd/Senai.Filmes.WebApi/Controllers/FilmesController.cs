using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        FilmeRepository filmeRepository = new FilmeRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(filmeRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(filmeRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpGet("{titulo}/buscar")]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            try
            {
                return Ok(filmeRepository.BuscarPorTitulo(titulo));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(FilmeDomain filme)
        {
            try
            {
                filmeRepository.Cadastrar(filme);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new {msg = "Ops..." + ex.Message});
            }
        }
    }
}