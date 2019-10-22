using Microsoft.EntityFrameworkCore;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
                            DataDeLancamento = Convert.ToDateTime(sdr["DataDeLancamento"]),
                            Poster = sdr["Poster"].ToString(),
                            NotaMedia = Convert.ToInt32(sdr["NotaMedia"])
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

        public List<LancamentoViewModel> ListarPorTitulo(string titulo)
        {
            List<LancamentoViewModel> lancamentos = new List<LancamentoViewModel>();
            string Query = "SELECT * FROM vmSelecionarDestintos WHERE Titulo LIKE @Titulo";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", "%" + titulo + "%");
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
                            DataDeLancamento = Convert.ToDateTime(sdr["DataDeLancamento"]),
                            NotaMedia = Convert.ToInt32(sdr["NotaMedia"]),
                            Poster = sdr["Poster"].ToString()
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

        public List<LancamentoViewModel> ListarFavoritos(int id)
        {
            return ListarLancamentoViewModel($"EXEC FavoritosPorIdUsuario @IdUsuario = {id}");
        }

        public Lancamentos BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Lancamentos.Include(x => x.Reviews).FirstOrDefault(x => x.IdLancamento == id);
            }
        }

        public void Favoritar(Favoritos favorito)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                foreach (var item in ctx.Favoritos.ToList())
                    if (item.IdLancamento == favorito.IdLancamento && item.IdUsuario == favorito.IdUsuario)
                    {
                        return;
                    }
                    else
                    {
                        ctx.Favoritos.Add(favorito);
                        ctx.SaveChanges();
                    }
            }
        }

        public void EscreverReview(Reviews review)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                int x = 0, z = 0;
                foreach (var item in ctx.Reviews.ToList())
                {
                    if(item.IdLancamento == review.IdLancamento && review.IdUsuario == item.IdUsuario)  return;
                    else if (item.IdLancamento == review.IdLancamento)
                    {
                        x += item.Nota;
                        z++;
                    }
                }
                ctx.Reviews.Add(review);
                int id = review.IdLancamento ?? default(int);
                var a = BuscarPorId(id);
                x = (x+review.Nota)/(z+1);
                a.NotaMedia = x;
                review.IdLancamentoNavigation = a;
                ctx.Lancamentos.Update(a);
                ctx.SaveChanges();
            }
        }

        public void DeletarReview(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Reviews review = ctx.Reviews.Find(id);
                ctx.Reviews.Remove(review);
                ctx.SaveChanges();
            }
        }

        public void CadastrarReview(Reviews review)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                ctx.Reviews.Add(review);
                ctx.SaveChanges();
            }
        }

        public void Cadastrar(CadastrarLancamentoViewModel lancamento)
        {
            if (lancamento.Poster != null && lancamento.Poster.Length > 0)
            {
                var NomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(lancamento.Poster.FileName);

                var CaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\imgs", NomeArquivo);

                using (var StreamImagem = new FileStream(CaminhoArquivo, FileMode.Create))
                {
                    lancamento.Poster.CopyTo(StreamImagem);
                }

                Lancamentos lancamentoTemp = new Lancamentos
                {
                    IdLancamento = lancamento.IdLancamento,
                    IdPlataforma = lancamento.IdPlataforma,
                    IdCategoria = lancamento.IdCategoria,
                    IdClassificacaoIndicativa = lancamento.IdClassificacaoIndicativa,
                    DataDeLancamento = lancamento.DataDeLancamento,
                    Episodios = lancamento.Episodios,
                    Sinopse = lancamento.Sinopse,
                    Titulo = lancamento.Titulo,
                    TipoDeMidia = lancamento.TipoDeMidia,
                    TempoDeDuracao = lancamento.TempoDeDuracao,
                    Poster = "/uploads/imgs/" + NomeArquivo
                };

                using (OpFlixContext ctx = new OpFlixContext())
                {
                    if (lancamento.TipoDeMidia == "F" && lancamento.Episodios != 1)
                        lancamento.Episodios = 1;
                    ctx.Lancamentos.Add(lancamentoTemp);
                    ctx.SaveChanges();
                }
            }
        }

        public void Atualizar(int id, CadastrarLancamentoViewModel lancamento)
        {
            if (lancamento.Poster != null && lancamento.Poster.Length > 0)
            {
                var NomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(lancamento.Poster.FileName);

                var CaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\imgs", NomeArquivo);

                using (var StreamImagem = new FileStream(CaminhoArquivo, FileMode.Create))
                {
                    lancamento.Poster.CopyTo(StreamImagem);
                }

                using (OpFlixContext ctx = new OpFlixContext())
                {
                    var a = BuscarPorId(id);
                    a.IdCategoria = lancamento.IdCategoria;
                    a.IdPlataforma = lancamento.IdPlataforma;
                    a.IdClassificacaoIndicativa = lancamento.IdClassificacaoIndicativa;
                    a.Titulo = lancamento.Titulo;
                    a.Sinopse = lancamento.Sinopse;
                    a.DataDeLancamento = lancamento.DataDeLancamento;
                    a.TipoDeMidia = lancamento.TipoDeMidia;
                    a.TempoDeDuracao = lancamento.TempoDeDuracao;
                    a.Poster = "/uploads/imgs/" + NomeArquivo;
                    if (a.TipoDeMidia == "F") a.Episodios = 1;
                    else a.Episodios = lancamento.Episodios;
                    ctx.Lancamentos.Update(a);
                    ctx.SaveChanges();
                }
            }
            else
            {
                using (OpFlixContext ctx = new OpFlixContext())
                {
                    var a = BuscarPorId(id);
                    a.IdCategoria = lancamento.IdCategoria;
                    a.IdPlataforma = lancamento.IdPlataforma;
                    a.IdClassificacaoIndicativa = lancamento.IdClassificacaoIndicativa;
                    a.Titulo = lancamento.Titulo;
                    a.Sinopse = lancamento.Sinopse;
                    a.DataDeLancamento = lancamento.DataDeLancamento;
                    a.TipoDeMidia = lancamento.TipoDeMidia;
                    a.TempoDeDuracao = lancamento.TempoDeDuracao;
                    if (a.TipoDeMidia == "F") a.Episodios = 1;
                    else a.Episodios = lancamento.Episodios;
                    ctx.Lancamentos.Update(a);
                    ctx.SaveChanges();
                }
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
