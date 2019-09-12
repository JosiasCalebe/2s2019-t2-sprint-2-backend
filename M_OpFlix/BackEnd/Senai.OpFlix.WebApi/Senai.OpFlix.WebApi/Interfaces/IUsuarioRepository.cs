using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        Usuarios BuscarPorEmailESenha(LoginViewModel login);
        void CadastrarAdmin(Usuarios usuario);
        void Cadastrar(Usuarios usuario);
        void Atualizar(int id, Usuarios usuario);
        void Atualizar(string nomeDeUsuario, Usuarios usuario);
        List<Usuarios> Listar();
    }
}
