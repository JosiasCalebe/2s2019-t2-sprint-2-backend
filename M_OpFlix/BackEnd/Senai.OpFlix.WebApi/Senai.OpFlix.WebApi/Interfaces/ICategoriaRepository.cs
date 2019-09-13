using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface ICategoriaRepository
    {
        /// <summary>
        /// Cadastra uma categoria.
        /// </summary>
        /// <param name="categoria">informações da categoria.</param>
        void Cadastrar(Categorias categoria);
        /// <summary>
        /// Lista as categoria.
        /// </summary>
        /// <returns>lista de categorias.</returns>
        List<Categorias> Listar();
        /// <summary>
        /// Atualiza uma categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <param name="categoria">informações da categoria.</param>
        void Atualizar(int id, Categorias categoria);
        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        void Deletar(int id);
        /// <summary>
        /// Busca uma categoria através do id.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <returns>uma categoria.</returns>
        Categorias BuscarPorId(int id);
    }
}
