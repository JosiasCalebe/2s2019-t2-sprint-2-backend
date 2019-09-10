using Microsoft.EntityFrameworkCore;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_OpFlix; User Id=sa;Pwd=132";
        public List<Lancamentos> ListarTodos()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Lancamentos.ToList();
            }
        }

        public List<LancamentoViewModel> ListarDestinto()
        {
            List<LancamentoViewModel> lancamentos = new List<LancamentoViewModel>();
            string Query = "SELECT * FROM vmSelecionarDestintos";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        LancamentoViewModel lancamento = new LancamentoViewModel
                        {
                            IdLancamento = Convert.ToInt32(sdr["IdLancamento"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Sinopse = sdr["Sinopse"].ToString(),
                            Categoria = sdr["Categoria"].ToString(),
                            ClassificacaoIndicativa = sdr["ClassificacaoIndicativa"].ToString(),
                            TempoDeDuracao = TimeSpan.Parse(sdr["TempoDeDuracao"].ToString()),
                            DataDeLancamento = Convert.ToDateTime(sdr["DataDeLancamento"])
                        };
                        if (sdr["TipoDeMidia"].ToString() == "F")
                        {
                            lancamento.TipoDeMidia = "Filme";
                            lancamento.Episodios = null;
                        }
                        else
                        {
                            lancamento.TipoDeMidia = "Série";
                            lancamento.Episodios = Convert.ToInt32(sdr["Episodios"]);
                        };
                        lancamentos.Add(lancamento);
                    }
                }
            }
            return lancamentos;
        }

        public Lancamentos BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var lista = ListarTodos();
                foreach (var item in lista)
                {
                    if(item.IdLancamento == id)
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        public void Cadastrar(Lancamentos lancamento)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                if(lancamento.TipoDeMidia == "F" && lancamento.Episodios != 1)
                {
                    lancamento.Episodios = 1;
                }
                ctx.Lancamentos.Add(lancamento);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(int id, Lancamentos lancamento)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var a = BuscarPorId(id);
                a.IdCategoria               = lancamento.IdCategoria;
                a.IdPlataforma              = lancamento.IdPlataforma;
                a.IdClassificacaoIndicativa = lancamento.IdClassificacaoIndicativa;
                a.Titulo                    = lancamento.Titulo;
                a.Sinopse                   = lancamento.Sinopse;
                a.DataDeLancamento          = lancamento.DataDeLancamento;
                a.TipoDeMidia               = lancamento.TipoDeMidia;
                a.TempoDeDuracao            = lancamento.TempoDeDuracao;
                a.Episodios                 = lancamento.Episodios;
                ctx.Lancamentos.Update(a);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Lancamentos lancamento = ctx.Lancamentos.Find(id);
                ctx.Lancamentos.Remove(lancamento);
                ctx.SaveChanges();
            }
        }
    }
}
