﻿using System;
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
            return Ok(LancamentoRepository.ListarTodos());
        }

        [HttpGet]
        public IActionResult ListarDestinto()
        {
            return Ok(LancamentoRepository.ListarDestinto());
        }
        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {
                if(LancamentoRepository.BuscarPorId(id) == null)
                {
                    return NotFound();
                }
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