using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface IPlataformaRepository
    {
        /// <summary>
        /// Cadastra uma plataforma.
        /// </summary>
        /// <param name="plataforma">informações da plataforma.</param>
        void Cadastrar(Plataformas plataforma);
        /// <summary>
        /// Lista as plataformas.
        /// </summary>
        /// <returns>lista de plataformas.</returns>
        List<Plataformas> Listar();
        /// <summary>
        /// Atualiza uma plataforma.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <param name="plataforma">informações da plataforma.</param>
        void Atualizar(int id, Plataformas plataforma);
        /// <summary>
        /// Deleta uma plataforma.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        void Deletar(int id);
        /// <summary>
        /// Busca uma plataforma através do id.
        /// </summary>
        /// <param name="id">id da plataforma.</param>
        /// <returns></returns>
        Plataformas BuscarPorId(int id);
    }
}
