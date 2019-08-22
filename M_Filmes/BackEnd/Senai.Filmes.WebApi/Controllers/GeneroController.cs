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
    public class GeneroController : ControllerBase
    {
        GeneroRepository generoRepository = new GeneroRepository();
        [HttpGet]
        public IEnumerable<GeneroDomain> Listar()
        {
            return generoRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(generoRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpGet("{nome}/buscar")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                return Ok(generoRepository.BuscarPorNome(nome));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpGet("{id}/filmes")]
        public IActionResult BuscarFilmes(int id)
        {
            try
            {
                return Ok(generoRepository.BuscarFilmes(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = "Ops..." + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            generoRepository.Cadastrar(genero);
            return Ok(genero);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(GeneroDomain generoDomain, int id)
        {
            generoRepository.Alterar(generoDomain, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            generoRepository.Deletar(id);
            return Ok();
        }







    }
}