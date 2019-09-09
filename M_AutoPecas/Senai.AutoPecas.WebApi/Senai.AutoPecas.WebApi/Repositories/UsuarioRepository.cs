using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel loginViewModel)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                Usuarios usuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Senha == loginViewModel.Senha);
                if (usuarioBuscado == null)
                    return null;
                return usuarioBuscado;
            }
        }

        public List<Usuarios> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
