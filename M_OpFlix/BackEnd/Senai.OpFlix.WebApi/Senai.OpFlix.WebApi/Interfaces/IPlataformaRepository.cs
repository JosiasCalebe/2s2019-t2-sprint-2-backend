using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces
{
    interface IPlataformaRepository
    {
        void Cadastrar(Plataformas plataforma);
        List<Plataformas> Listar();
        void Atualizar(int id, Plataformas plataforma);
        void Deletar(int id);
        Plataformas BuscarPorId(int id);
    }
}
