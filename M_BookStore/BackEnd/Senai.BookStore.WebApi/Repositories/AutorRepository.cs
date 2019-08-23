using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class AutorRepository
    {
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";

        public void Cadastrar(AutorDomain autor)
        {
            string Query = "INSERT INTO Autores(Nome, Email, Ativo, DataNascimento) VALUES (@Nome, @Email, @Ativo, @DataNascimento)";
            using (SqlConnection
                 con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", autor.Nome);
                cmd.Parameters.AddWithValue("@Email", autor.Email);
                cmd.Parameters.AddWithValue("@Ativo", autor.Ativo);
                cmd.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<AutorDomain> Listar()
        {
            List<AutorDomain> autores = new List<AutorDomain>();
            string Query = "SELECT IdAutor, Nome, Email, Ativo, DataNascimento FROM Autores";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = Convert.ToBoolean(sdr["Ativo"]),
                            DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                        };
                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }

        public List<AutorDomain> ListarAtivos()
        {
            List<AutorDomain> autores = new List<AutorDomain>();
            string Query = "SELECT IdAutor, Nome, Email, Ativo, DataNascimento FROM Autores WHERE Ativo = 1";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = Convert.ToBoolean(sdr["Ativo"]),
                            DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                        };
                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }

        public List<AutorDomain> ListarAnos90()
        {
            List<AutorDomain> autores = new List<AutorDomain>();
            string Query = "SELECT IdAutor, Nome, Email, Ativo, DataNascimento FROM Autores WHERE DataNascimento >= '01/01/1990'";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = Convert.ToBoolean(sdr["Ativo"]),
                            DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                        };
                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }

        public List<LivroDomain> BuscarPorAno(int ano)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor WHERE A.DataNascimento >= @IdAutor";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", "01/01/"+ano.ToString());
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

        public List<LivroDomain> BuscarLivros(int id)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor WHERE L.IdAutor = @IdAutor";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", id);
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

        public List<LivroDomain> BuscarLivrosAtivos(int id)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor WHERE L.IdAutor = @IdAutor AND A.Ativo = 1";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", id);
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


    }
}
