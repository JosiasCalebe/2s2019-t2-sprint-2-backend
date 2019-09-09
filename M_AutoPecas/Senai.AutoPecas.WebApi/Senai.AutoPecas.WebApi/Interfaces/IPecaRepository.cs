using Senai.AutoPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Interfaces
{
    interface IPecaRepository
    {
        List<Pecas> Listar();
        void Cadastrar(Pecas peca);
        void Atualizar(Pecas peca);
        void Deletar(int id);
        Pecas BuscarPorId(int id);
        void CalcularGanho();
        float CalcularValor(int unidades, int id);
    }
}
