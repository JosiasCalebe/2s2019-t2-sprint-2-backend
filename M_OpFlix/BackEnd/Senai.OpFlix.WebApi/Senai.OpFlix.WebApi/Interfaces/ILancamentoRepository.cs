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
        List<Lancamentos> ListarTodos();
        List<LancamentoViewModel> ListarDestinto();
        Lancamentos BuscarPorId(int id);
        void Cadastrar(Lancamentos lancamento);
        void Atualizar(int id, Lancamentos lancamento);
        void Deletar(int id);

    }
}
