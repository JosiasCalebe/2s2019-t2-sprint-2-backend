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

        public List<Lancamentos> ListarLancamentos()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Lancamentos.ToList();
            }
        }

        public List<LancamentoViewModel> ListarLancamentoViewModel(string query)
        {
            List<LancamentoViewModel> lancamentos = new List<LancamentoViewModel>();
            string Query = query;
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

                        try
                        {
                            lancamento.Plataforma = sdr["Plataforma"].ToString();
                        }
                        catch (Exception)
                        {
                            lancamento.Plataforma = null;
                        }

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

        public List<LancamentoViewModel> ListarDestinto(int? id)
        {
            if(id == null)
                return ListarLancamentoViewModel("SELECT * FROM vmSelecionarDestintos");
            else
                return ListarLancamentoViewModel($"EXEC SelecionarDestintosPorIdUsuario @IdUsuario = {id}");
        }

        public List<LancamentoViewModel> ListarPorIdCategoria(int id)
        {
            return ListarLancamentoViewModel($"EXEC LancamentosPorIdCategoria @IdCategoria = {id}");
        }

        public List<LancamentoViewModel> ListarPorIdPlataforma(int id)
        {
            return ListarLancamentoViewModel($"EXEC LancamentosPorIdPlataforma @IdPlataforma = {id}");
        }

        public List<LancamentoViewModel> ListarPorData(string data)
        {
            data = $"'{data}'";
            return ListarLancamentoViewModel($"EXEC LancamentosPorDataDeLancamento @Data = {data}");
        }

        public List<LancamentoViewModel> ListarFavoritos(int id)
        {
            return ListarLancamentoViewModel($"EXEC FavoritosPorIdUsuario @IdUsuario = {id}");
        }

        public Lancamentos BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Lancamentos.Find(id);
            }
        }

        public void Favoritar(Favoritos favorito)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                ctx.Favoritos.Add(favorito);
                ctx.SaveChanges();
            }
        }

        public void Cadastrar(Lancamentos lancamento)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                if(lancamento.TipoDeMidia == "F" && lancamento.Episodios != 1)
                    lancamento.Episodios = 1;
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
                if(a.TipoDeMidia == "F")    a.Episodios = 1;
                else a.Episodios            = lancamento.Episodios;
                ctx.Lancamentos.Update(a);
                ctx.SaveChanges();
            }
        }

        public void Desfavoritar(Favoritos favorito)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                foreach (var item in ctx.Favoritos.ToList())
                    if (item.IdLancamento == favorito.IdLancamento && item.IdUsuario == favorito.IdUsuario)
                    {
                        ctx.Favoritos.Remove(item);
                        ctx.SaveChanges();
                    }
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
