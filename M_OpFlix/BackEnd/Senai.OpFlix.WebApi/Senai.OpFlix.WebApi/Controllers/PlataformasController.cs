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
        /// Cadastra uma plataforma.
        /// </summary>
        /// <param name="plataforma"></param>
        /// <returns>status Ok</returns>
        [HttpPost]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar(Plataformas plataforma)
        {
            try
            {
                PlataformaRepository.Cadastrar(plataforma);
                return Ok(new { mensagem = "Plataforma cadastrada com sucesso!" });
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Listar()
        {
            try
            {
                if (PlataformaRepository.Listar() == null)
                    return NotFound(new { mensagem = "Plataformas não encontradas!" });
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Buscar(int id)
        {
            try
            {
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Plataforma não encontrada!" });
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, Plataformas plataforma)
        {
            try
            {
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Plataforma não encontada!" });
                PlataformaRepository.Atualizar(id, plataforma);
                return Ok(new { mensagem = "Plataforma atualizada com sucesso!" });
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (PlataformaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Plataforma não encontrada!" });
                PlataformaRepository.Deletar(id);
                return Ok(new { mensagem = "Plataforma deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}