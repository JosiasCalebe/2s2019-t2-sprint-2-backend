using System;
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

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(UsuarioRepository.Listar());
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarAdmin(Usuarios usuario)
        {
            try
            {
                string TipoUsuario;
                try
                {
                    TipoUsuario = HttpContext.User.Claims.First(x => x.Type == "TipoDeUsuario").Value;
                }
                catch (Exception)
                {
                    TipoUsuario = null;
                }
                if (TipoUsuario == "A"){
                    UsuarioRepository.CadastrarAdmin(usuario);
                }
                else if(TipoUsuario == null)
                {
                    UsuarioRepository.Cadastrar(usuario);

                }
                else
                {
                    return Forbid();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { mensagem = "Erro " + ex.Message });
            }
        }


        [HttpPost("login")]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuarios Usuario = UsuarioRepository.BuscarPorEmailESenha(login);
                if (Usuario == null)
                {
                    return NotFound(new { mensagem = "Email ou senha inválidos" });
                }
                var claims = new[]
                {

                    new Claim(JwtRegisteredClaimNames.Email, Usuario.Email),

                    new Claim(JwtRegisteredClaimNames.Jti, Usuario.IdUsuario.ToString()),

                    new Claim(ClaimTypes.Role, Usuario.Tipo),
                    new Claim("TipoDeUsuario", Usuario.Tipo),
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
    }
}