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
    public class LancamentosController : ControllerBase
    {
        private ILancamentoRepository LancamentoRepository { get; set; }
        public LancamentosController()
        {
            LancamentoRepository = new LancamentoRepository();
        }

        [HttpGet("todos")]
        public IActionResult ListarTodos()
        {
            if (LancamentoRepository.ListarLancamentos() == null)
                return NoContent();
            return Ok(LancamentoRepository.ListarLancamentos());
        }

        [HttpGet]
        public IActionResult ListarDestinto()
        {
            if (LancamentoRepository.ListarDestinto() == null)
                return NoContent();
            return Ok(LancamentoRepository.ListarDestinto());
        }

        [HttpGet("categorias/{id}")]
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

        [HttpGet("plataformas/{id}")]
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
        [HttpGet("data/{data}")]
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

        [HttpGet("favoritos")]
        [Authorize]
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

        [HttpPost("favoritos")]
        [Authorize]
        public IActionResult Favoritar(Favoritos favorito)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                favorito.IdUsuario = idUsuario;
                LancamentoRepository.Favoritar(favorito);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });

            }
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {
                if(LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound();
                return Ok(LancamentoRepository.BuscarPorId(id));
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public IActionResult Cadastrar(Lancamentos lancamento)
        {
            try
            {
                LancamentoRepository.Cadastrar(lancamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "A")]
        public IActionResult Atualizar(int id, Lancamentos lancamento)
        {
            try
            {
                if (LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound();
                LancamentoRepository.Atualizar(id, lancamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "A")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (LancamentoRepository.BuscarPorId(id) == null)
                    return NotFound();
                LancamentoRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}