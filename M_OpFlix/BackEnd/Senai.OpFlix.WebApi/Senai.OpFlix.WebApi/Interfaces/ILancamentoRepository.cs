﻿using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface ILancamentoRepository
    {
        /// <summary>
        /// Lista todos os lançamentos.
        /// </summary>
        /// <returns>Lista de lançamentos.</returns>
        List<Lancamentos> ListarLancamentos();
        /// <summary>
        /// Lista os lançamentos através de uma query de SQL
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>Lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarLancamentoViewModel(string query);
        /// <summary>
        /// Lista os lançamentos.
        /// </summary>
        /// <returns>lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarDestinto();
        /// <summary>
        /// Lista os lançamentos através.
        /// </summary>
        /// <param name="id">id da categoria.</param>
        /// <returns>Lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarPorIdCategoria(int id);
        /// <summary>
        /// Lista os lançamentos através da plataforma.
        /// </summary>
        /// <param name="id">id da paltaforma.</param>
        /// <returns>Lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarPorIdPlataforma(int id);
        /// <summary>
        /// Lista os lançamentos através da data de lançamento.
        /// </summary>
        /// <param name="data">data de lançamento.</param>
        /// <returns>Lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarPorData(string data);
        /// <summary>
        /// Lista os favoritos de um usuário.
        /// </summary>
        /// <param name="id">id do usuário.</param>
        /// <returns>Lista de lançamentos.</returns>
        List<LancamentoViewModel> ListarFavoritos(int id);
        /// <summary>
        /// Favorita um lançamento.
        /// </summary>
        /// <param name="favorito">informações do favorito.</param>
        void Favoritar(Favoritos favorito);
        /// <summary>
        /// Busca um lançamento através do id.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
        /// <returns>um lançamento.</returns>
        Lancamentos BuscarPorId(int id);
        /// <summary>
        /// Cadastra um lançamento.
        /// </summary>
        /// <param name="lancamento">informações do lançamento</param>
        void Cadastrar(Lancamentos lancamento);
        /// <summary>
        /// Atualiza um lançamento.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
        /// <param name="lancamento">informações do lançamento</param>
        void Atualizar(int id, Lancamentos lancamento);
        /// <summary>
        /// Deleta um lançamento.
        /// </summary>
        /// <param name="id">id do lançamento.</param>
        void Deletar(int id);

    }
}