using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.Repositories;

namespace Senai.OpFlix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private ICategoriaRepository CategoriaRepository { get; set; }
        CategoriasController()
        {
            CategoriaRepository = new CategoriaRepository();
        }


        [HttpPost]
        [Authorize(Roles = "A")]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                if (CategoriaRepository.Listar() == null) return NotFound();
                return Ok(CategoriaRepository.Listar());
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


        [HttpPut]
        [Authorize(Roles = "A")]
        public IActionResult Atualizar(int id, Categorias categoria)
        {
            try
            {
                if (CategoriaRepository.BuscarPorId(id) == null)
                    return NotFound();
                CategoriaRepository.Atualizar(id, categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


        [HttpDelete]
        [Authorize(Roles = "A")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (CategoriaRepository.BuscarPorId(id) == null)
                    return NotFound();
                CategoriaRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}