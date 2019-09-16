using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public void Cadastrar(Categorias categoria)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        public List<Categorias> Listar()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var lista = ctx.Categorias.ToList();
                foreach(var item in lista)
                    item.Lancamentos = null;
                return lista;
            }
        }

        public void Atualizar(int id, Categorias categoria)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var a = ctx.Categorias.Find(id);
                a.Categoria = categoria.Categoria;
                ctx.Categorias.Update(a);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Categorias categoria = ctx.Categorias.Find(id);
                ctx.Categorias.Remove(categoria);
                ctx.SaveChanges();
            }
        }

        public Categorias BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var item = ctx.Categorias.Find(id);
                item.Lancamentos = null;
                return item;
            }
        }
    }
}
