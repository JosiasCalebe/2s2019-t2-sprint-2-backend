using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class GeneroRepository
    {
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore; User Id=sa;Pwd=132";

        public void Cadastrar(GeneroDomain genero)
        {
            string Query = "INSERT INTO Generos (Descricao) VALUES (@Descricao)";
            using (SqlConnection
                 con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Descricao", genero.Descricao);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<GeneroDomain> Listar()
        {
            List<GeneroDomain> generos = new List<GeneroDomain>();
            string Query = "SELECT IdGenero, Descricao FROM Generos";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Descricao = sdr["Descricao"].ToString()
                        };
                        generos.Add(genero);
                    }
                }
            }
            return generos;
        }

        public List<LivroDomain> Buscar(string genero)
        {
            List<LivroDomain> livros = new List<LivroDomain>();
            string Query = "SELECT L.IdLivro, L.Descricao AS Titulo, L.IdAutor, L.IdGenero, A.Nome AS Autor, A.Email, A.Ativo, A.DataNascimento, G.Descricao AS Genero FROM Livros L INNER JOIN Generos G ON L.IdGenero = G.IdGenero INNER JOIN Autores A ON L.IdAutor = A.IdAutor WHERE G.Descricao LIKE @Genero";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Genero", "%" + genero + "%");
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
