using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class LivroRepository
    {
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";

        public void Cadastrar(LivroDomain livro)
        {
            string Query = "INSERT INTO Livros (Descricao, IdAutor, IdGenero) VALUES (@Descricao, @IdAutor, @IdGenero)";
            using (SqlConnection
                 con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Descricao", livro.Descricao);
                cmd.Parameters.AddWithValue("@IdAutor", livro.IdAutor);
                cmd.Parameters.AddWithValue("@IdGenero", livro.IdGenero);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<LivroDomain> Listar()
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Descricao = sdr["Titulo"].ToString(),
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Autor"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                            },
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Genero"].ToString()
                            }

                        };
                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }

        public LivroDomain Buscar(int id)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor WHERE IdLivro = @IdLivro";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdLivro",id);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Descricao = sdr["Titulo"].ToString(),
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Autor"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                            },
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Genero"].ToString()
                            }

                        };
                        return livro;
                    }
                    return null;
                }
            }
        }

        public void Deletar(int id)
        {
            string Query = "DELETE FROM Livros WHERE IdLivro = @IdLivro";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdLivro", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(LivroDomain livro, int id)
        {
            string Query = "UPDATE Livros SET Descricao = @Descricao, IdAutor = @IdAutor, IdGenero = @IdGenero WHERE IdLivro = @IdLivro";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdLivro", id);
                cmd.Parameters.AddWithValue("@Descricao", livro.Descricao);
                cmd.Parameters.AddWithValue("@IdAutor", livro.IdAutor);
                cmd.Parameters.AddWithValue("@IdGenero", livro.IdGenero);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
