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
        public CategoriasController()
        {
            CategoriaRepository = new CategoriaRepository();
        }

        /// <summary>
        /// Cadastra uma categoria.
        /// </summary>
        /// <param name="categoria">informações da categoria.</param>
        /// <returns>status Ok</returns>
        [HttpPost]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok(new { mensagem = "Categoria cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Lista as categorias.
        /// </summary>
        /// <returns>lista de categorias.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Listar()
        {
            try
            {
                if (CategoriaRepository.Listar() == null)
                    return NotFound(new { mensagem = "Categorias não encontradas!" });
                return Ok(CategoriaRepository.Listar());
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <returns>uma categoria.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Buscar(int id)
        {
            try
            {
                if (CategoriaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Categoria não encontrada!" });
                return Ok(CategoriaRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as informações de uma categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <param name="categoria">informações da categoria.</param>
        /// <returns>status Ok</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, Categorias categoria)
        {
            try
            {
                if (CategoriaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Categoria não encontrada!" });
                CategoriaRepository.Atualizar(id, categoria);
                return Ok(new { mensagem = "Categoria atualizada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
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
                if (CategoriaRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Categoria não encontrada!" });
                CategoriaRepository.Deletar(id);
                return Ok(new { mensagem = "Categoria deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}