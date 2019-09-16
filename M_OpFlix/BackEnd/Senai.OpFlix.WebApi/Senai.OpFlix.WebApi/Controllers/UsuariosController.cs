﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.Repositories;
using Senai.OpFlix.WebApi.Utils;
using Senai.OpFlix.WebApi.ViewModels;

namespace Senai.OpFlix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }
        public UsuariosController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os usuários.
        /// </summary>
        /// <returns>lista de usuários.</returns>
        [HttpGet]
        [Authorize(Roles = "A")]
        public IActionResult Listar()
        {
            if (UsuarioRepository.Listar() == null)
                return NotFound();
            return Ok(UsuarioRepository.Listar());
        }

        /// <summary>
        /// Cadastra um usuário.
        /// </summary>
        /// <param name="usuario">informações do usuário.</param>
        /// <returns>status Ok</returns>
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Usuarios usuario)
        {
            try
            {
                string TipoUsuario;
                try
                {
                    TipoUsuario = HttpContext.User.Claims.First(x => x.Type == "TipoDeUsuario").Value;
                    if (TipoUsuario == "A")
                        UsuarioRepository.CadastrarAdmin(usuario);
                    else
                        return Forbid();
                }
                catch (Exception)
                {
                    TipoUsuario = null;
                    UsuarioRepository.Cadastrar(usuario);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Faz o login um usuário.
        /// </summary>
        /// <param name="login">informações de login.</param>
        /// <returns>status Ok</returns>
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuarios Usuario = UsuarioRepository.BuscarPorEmailESenha(login);
                if (Usuario == null)
                    return NotFound(new { mensagem = "Email ou senha inválidos" });
                var claims = new[]
                {

                    new Claim(JwtRegisteredClaimNames.Email, Usuario.Email),

                    new Claim(JwtRegisteredClaimNames.Jti, Usuario.IdUsuario.ToString()),

                    new Claim(ClaimTypes.Role, Usuario.Tipo),
                    new Claim("TipoDeUsuario", Usuario.Tipo),
                    new Claim("IdUsuario", Usuario.IdUsuario.ToString()),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("opflix-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Senai.OpFlix.WebApi",
                    audience: "Senai.OpFlix.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro " + ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as iformações do usuário logado.
        /// </summary>
        /// <param name="usuario">informações do usuário.</param>
        /// <returns>status Ok</returns>
        [HttpPut]
        [Authorize]
        public IActionResult AtualizarUsuario(Usuarios usuario)
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
                UsuarioRepository.Atualizar(idUsuario, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um usuário através do id.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        /// <param name="usuario">informações do usuário.</param>
        /// <returns>status Ok</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "A")]
        public IActionResult AtualizarUsuario(int id, Usuarios usuario)
        {
            try
            {
                if (UsuarioRepository.BuscarPorId(id) == null)
                    return NotFound();
                UsuarioRepository.Atualizar(id, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um usuário atráves do id.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        /// <returns>status Ok</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "A")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (UsuarioRepository.BuscarPorId(id) == null)
                    return NotFound();
                UsuarioRepository.Deletar(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um usuário logado.
        /// </summary>
        /// <returns>status Ok</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Deletar()
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "IdUsuario").Value);
            UsuarioRepository.Deletar(idUsuario);
            return Ok();
        }
    }
}