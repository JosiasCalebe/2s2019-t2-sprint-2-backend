using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories
{
    public class FilmeRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=RoteiroFilmes; User Id=sa;Pwd=132";

        public List<FilmeDomain> Listar()
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();
            string Query = "SELECT F.IdFilme, F.Titulo, F.IdGenero, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Nome = sdr["Nome"].ToString()
                            }
                        };
                        filmes.Add(filme);
                    }

                }
            }
            return filmes;
        }

        public List<FilmeDomain> BuscarPorId(int id)
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();
            string Query = "SELECT F.IdFilme, F.Titulo, F.IdGenero, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE IdFilme = @IdFilme";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme",id);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Nome = sdr["Nome"].ToString()
                            }
                        };
                        filmes.Add(filme);
                    }

                }
            }
            return filmes;
        }

        public List<FilmeDomain> BuscarPorTitulo(string titulo)
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();
            string Query = "SELECT F.IdFilme, F.Titulo, F.IdGenero, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE Titulo LIKE @Titulo";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", "%" + titulo + "%");
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Nome = sdr["Nome"].ToString()
                            }
                        };
                        filmes.Add(filme);
                    }

                }
            }
            return filmes;
        }


        public void Cadastrar(FilmeDomain filme)
        {
            string Query = "INSERT INTO Filmes(Titulo, IdGenero) VALUES (@Titulo, @IdGenero)";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
