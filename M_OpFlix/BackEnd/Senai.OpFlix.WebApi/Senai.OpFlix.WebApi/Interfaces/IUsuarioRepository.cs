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
        /// <summary>
        /// Busca um usuário pelo email e senha.
        /// </summary>
        /// <param name="login">email e senha inseridos pelo usuários.</param>
        /// <returns></returns>
        Usuarios BuscarPorEmailESenha(LoginViewModel login);
        /// <summary>
        /// Busca um usuário através do id.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        /// <returns>informações de um usuário.</returns>
        Usuarios BuscarPorId(int id);
        /// <summary>
        /// Cadastra um usuário que pode ser admin.
        /// </summary>
        /// <param name="usuario">informações do usuário.</param>
        void CadastrarAdmin(Usuarios usuario);
        /// <summary>
        /// Cadastra um usuário.
        /// </summary>
        /// <param name="usuario">informações do usuário.</param>
        void Cadastrar(Usuarios usuario);
        /// <summary>
        /// Atualiza as informações do usuário.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        /// <param name="usuario">informações do usuário.</param>
        void Atualizar(int id, Usuarios usuario);
        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        void Deletar(int id);
        /// <summary>
        /// Lista os usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        List<Usuarios> Listar();
    }
}
