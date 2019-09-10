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
    public class PlataformasController : ControllerBase
    {
        private IPlataformaRepository PlataformaRepository { get; set; }
        public PlataformasController()
        {
            PlataformaRepository = new PlataformaRepository();
        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public IActionResult Cadastrar(Plataformas plataforma)
        {
            try
            {
                PlataformaRepository.Cadastrar(plataforma);
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
                if (PlataformaRepository.Listar() == null) return NotFound();
                return Ok(PlataformaRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


        [HttpPut]
        [Authorize(Roles = "A")]
        public IActionResult Atualizar(int id, Plataformas plataforma)
        {
            try
            {
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound();
                PlataformaRepository.Atualizar(id, plataforma);
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
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound();
                PlataformaRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}