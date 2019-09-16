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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plataforma"></param>
        /// <returns>status Ok</returns>
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

        /// <summary>
        /// Lista as plataformas.
        /// </summary>
        /// <returns>lista de plataformas.</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                if (PlataformaRepository.Listar() == null)
                    return NotFound();
                return Ok(PlataformaRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma plataforma através do id.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <returns>uma plataforma.</returns>
        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound();
                return Ok(PlataformaRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as informações de uma plataforma.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <param name="plataforma">informações da plataforma.</param>
        /// <returns>status Ok</returns>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Deleta uma plataforma.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <returns>status Ok</returns>
        [HttpDelete("{id}")]
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