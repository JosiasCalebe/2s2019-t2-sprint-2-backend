using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface ILancamentoRepository
    {
        List<Lancamentos> ListarLancamentos();
        List<LancamentoViewModel> ListarLancamentoViewModel(string query);
        List<LancamentoViewModel> ListarDestinto();
        List<LancamentoViewModel> ListarPorIdCategoria(int id);
        List<LancamentoViewModel> ListarPorIdPlataforma(int id);
        List<LancamentoViewModel> ListarPorData(string data);
        List<LancamentoViewModel> ListarFavoritos(int id);
        void Favoritar(Favoritos favorito);
        Lancamentos BuscarPorId(int id);
        void Cadastrar(Lancamentos lancamento);
        void Atualizar(int id, Lancamentos lancamento);
        void Deletar(int id);

    }
}
