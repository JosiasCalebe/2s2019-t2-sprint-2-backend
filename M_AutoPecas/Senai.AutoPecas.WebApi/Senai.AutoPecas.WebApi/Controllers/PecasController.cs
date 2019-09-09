using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private IPecaRepository PecaRepository { get; set; }

        public PecasController()
        {
            PecaRepository = new PecaRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(PecaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(Pecas peca)
        {
            try
            {
                PecaRepository.Cadastrar(peca);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Pecas peca)
        {
            try
            {
                if (PecaRepository.BuscarPorId(id) == null)
                    return NotFound();
                PecaRepository.Atualizar(peca);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (PecaRepository.BuscarPorId(id) == null)
                    return NotFound();
               PecaRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("calcular")]
        public IActionResult CalcularValor(int unidades, int id)
        {
            try
            {
                return Ok(PecaRepository.CalcularValor(unidades,id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}