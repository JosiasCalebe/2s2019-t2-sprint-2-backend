using Microsoft.EntityFrameworkCore;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class PecaRepository : IPecaRepository
    {
        public void Atualizar(Pecas p)
        {
            using(AutoPecasContext ctx = new AutoPecasContext())
            {
                var a = ctx.Pecas.Find(p.PecaId);
                a.Codigo = p.Codigo;
                a.Descricao = p.Descricao;
                a.Peso = p.Peso;
                a.PrecoCusto = p.PrecoCusto;
                a.PrecoVenda = p.PrecoVenda;
                a.FornecedorId = p.FornecedorId;

                ctx.Pecas.Update(a);
                ctx.SaveChanges();
            }
        }

        public Pecas BuscarPorId(int id)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                var peca = ctx.Pecas.Find(id);
                peca.Fornecedor.Pecas = null;
                return peca;
            }
        }

        public void Cadastrar(Pecas peca)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                ctx.Pecas.Add(peca);
                ctx.SaveChanges();
            }
        }

        public void CalcularGanho()
        {
            throw new NotImplementedException();
        }

        public float CalcularValor(int unidades, int id)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                var p = ctx.Pecas.Find(id);
                return p.PrecoVenda * unidades;
            }
        }

        public void Deletar(int id)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                ctx.Pecas.Remove(ctx.Pecas.Find(id));
                ctx.SaveChanges();
            }
        }

        public List<Pecas> Listar()
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                var listaPecas = ctx.Pecas.Include(x => x.Fornecedor).ToList();
                List<Pecas> Pecas = new List<Pecas>();

                foreach (var p in listaPecas)
                {
                    var a = new Pecas();
                    a = p;
                    a.Fornecedor.Pecas = null;
                    Pecas.Add(a);
                }
                return Pecas;
            }

            
        }
    }
}
