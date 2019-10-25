using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.Repositories;
using Senai.OpFlix.WebApi.ViewModels;

namespace Senai.OpFlix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private ILancamentoRepository LancamentoRepository { get; set; }
        public LancamentosController()
        {
            LancamentoRepository = new LancamentoRepository();
        }

        /// <summary>
        /// Lista todos os lançamentos.
        /// </summary>
        /// <returns>lista de lançamentos.</returns>
        [HttpGet("todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult ListarTodos()
        {
            if (LancamentoRepository.ListarLancamentos() == null)
                return NoContent();
            return Ok(LancamentoRepository.ListarLancamentos());
        }

        /// <summary>
        /// Lista os lançamentos sem repetição.
        /// </summary>
        /// <returns>lista de lançamentos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarDestinto()
        {
            try
            {
                int idUsuario;
                try
                {
                    idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                    if (LancamentoRepository.ListarDestinto(idUsuario) == null)
                        return NoContent();
                    return Ok(LancamentoRepository.ListarDestinto(idUsuario));
                }
                catch (Exception)
                {
                    if (LancamentoRepository.ListarDestinto(null) == null)
                        return NoContent();
                    return Ok(LancamentoRepository.ListarDestinto(null));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("buscar/{titulo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarPorTitulo(string titulo)
        {
            try
            {
                if (LancamentoRepository.ListarPorTitulo(titulo) == null)
                    return NoContent();
                return Ok(LancamentoRepository.ListarPorTitulo(titulo));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Lista os lançamentos de acordo com o id da categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <returns>lista de lançamentos.</returns>
        [HttpGet("categorias/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarPorIdCategoria(int id)
        {
            try
            {
                if (LancamentoRepository.ListarPorIdCategoria(id) == null)
                    return NoContent();
                return Ok(LancamentoRepository.ListarPorIdCategoria(id));
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Lista os lançamentos de acordo com o id da plataforma.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <returns>lista de lançamentos.</returns>
        [HttpGet("plataformas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarPorIdPlataforma(int id)
        {
            try
            {
                if (LancamentoRepository.ListarPorIdPlataforma(id) == null)
                    return NoContent();
                return Ok(LancamentoRepository.ListarPorIdPlataforma(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Lista os lançamento de acordo com a data inserida.
        /// </summary>
        /// <param name="data">data.</param>
        /// <returns>lista de lançamentos</returns>
        [HttpGet("data/{data}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarPorData(string data)
        {
            try
            {
                if (!DateTime.TryParse(data, out DateTime date))
                    return BadRequest(new { mensagem = "data inválida" });
                if (LancamentoRepository.ListarPorData(data) == null)
                    return NoContent();
                return Ok(LancamentoRepository.ListarPorData(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Lista os favoritos do usuário logado.
        /// </summary>
        /// <returns>lista de favoritos do usuário logado.</returns>
        [HttpGet("favoritos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarFavoritos()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                if (LancamentoRepository.ListarFavoritos(idUsuario) == null)
                    return NoContent();
                return Ok(LancamentoRepository.ListarFavoritos(idUsuario));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Busca um lançamento através do id.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
        /// <returns>um lançamento.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Buscar(int id)
        {
            try
            {
                if (LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Lançamento não encontrado!" });
                return Ok(LancamentoRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Favorita o lançamento no usuário logado.
        /// </summary>
        /// <param name="favorito">informações do favorito.</param>
        /// <returns>status Ok</returns>
        [HttpPost("favoritos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Favoritar(Favoritos favorito)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                favorito.IdUsuario = idUsuario;
                LancamentoRepository.Favoritar(favorito);
                return Ok(new { mensagem = "Lançamento favoritado!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        [HttpGet("favoritos/{idL}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChecarFAvorito(int idL)
        {
            try
            {
                int idU = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                return Ok(LancamentoRepository.ChecarFavorito(idU, idL));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EscreverReview(int id, Reviews review)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                review.IdUsuario = idUsuario;
                review.IdLancamento = id;
                LancamentoRepository.EscreverReview(review);
                return Ok(new { mensagem = "Review cadastrada!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Cadastra um lançamento.
        /// </summary>
        /// <param name="lancamento">informações do lançamento.</param>
        /// <returns>status Ok</returns>
        [HttpPost]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar([FromForm] CadastrarLancamentoViewModel lancamento)
        {
            try
            {
                LancamentoRepository.Cadastrar(lancamento);
                return Ok(new { mensagem = "Lançamento cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Desfavorita um lançamento do usuário logado.
        /// </summary>
        /// <param name="favorito">informações do favorito.</param>
        /// <returns>status Ok</returns>
        [HttpDelete("favoritos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Desfavoritar(Favoritos favorito)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                favorito.IdUsuario = idUsuario;
                LancamentoRepository.Desfavoritar(favorito);
                return Ok(new { mensagem = "Lançamento desfavoritado!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Deleta um lançamento.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
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
                if (LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Lançamento não encontrado!" });
                LancamentoRepository.Deletar(id);
                return Ok(new { mensagem = "Lançamento deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as informações de um lançamento.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
        /// <param name="lancamento">informações do lançamento.</param>
        /// <returns>status Ok</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, [FromForm] CadastrarLancamentoViewModel lancamento)
        {
            try
            {
                if (LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound(new { mensagem = "Lançamento não encontrado!" });
                LancamentoRepository.Atualizar(id, lancamento);
                return Ok(new { mensagem = "Lançamento atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}