using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {

        public void Cadastrar(Plataformas plataforma)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                ctx.Plataformas.Add(plataforma);
                ctx.SaveChanges();
            }
        }

        public List<Plataformas> Listar()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Plataformas.ToList();
            }
        }

        public void Atualizar(int id, Plataformas plataforma)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var a = ctx.Plataformas.Find(id);
                a.Plataforma = plataforma.Plataforma;
                ctx.Plataformas.Update(a);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Plataformas plataforma = ctx.Plataformas.Find(id);
                ctx.Plataformas.Remove(plataforma);
                ctx.SaveChanges();
            }
        }
        public Plataformas BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Plataformas.Find(id);
            }
        }
    }
}
