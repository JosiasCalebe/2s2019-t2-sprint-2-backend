﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Listar()
        {
            return funcionarioRepository.Listar();
        }
        [HttpGet("nomescompletos")]
        public IEnumerable<FuncionarioDomain> ListarNomesCompletos()
        {
            return funcionarioRepository.ListarNomesCompletos();
        }

        [HttpGet("ordenacao/{ordem}")]
        public IActionResult ListarOrdenado(string ordem)
        {
            ordem = ordem.ToUpper();
            if (ordem != "ASC" && ordem != "DESC")
            {
                return BadRequest();
            }
            List<FuncionarioDomain> funcionarios = funcionarioRepository.ListarOrdenado(ordem);
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FuncionarioDomain funcionario = funcionarioRepository.BuscarPorId(id);
            if(funcionario == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(funcionario);
            }
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            List<FuncionarioDomain> funcionarios = funcionarioRepository.BuscarPorNome(nome);
            if (funcionarios == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(funcionarios);
            }
        }

        [HttpPost]
        public IActionResult Inserir(FuncionarioDomain funcionario)
        {
            funcionarioRepository.Inserir(funcionario);
            return Ok(funcionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(FuncionarioDomain funcionario, int id)
        {
            funcionarioRepository.Alterar(funcionario, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            funcionarioRepository.Deletar(id);
            return Ok();
        }
    }
}