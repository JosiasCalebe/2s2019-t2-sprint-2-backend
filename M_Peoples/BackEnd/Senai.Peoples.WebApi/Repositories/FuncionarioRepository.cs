using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository
    {
        List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>()
        {
            new FuncionarioDomain {IdFuncionario = 1, Nome = "Catarina", Sobrenome = "Strada", DataDeNascimento = DateTime.Parse("11/04/1990") }
            ,new FuncionarioDomain {IdFuncionario = 2, Nome = "Tadeu", Sobrenome = "Vitelli", DataDeNascimento = DateTime.Parse("09/01/1995") }
        };
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_Peoples; User Id=sa;Pwd=132";

        public void Inserir(FuncionarioDomain funcionario)
        {
            string Query = "INSERT INTO Funcionarios (Nome, Sobrenome, DataDeNascimento) VALUES (@Nome, @Sobrenome, @DataDeNascimento)";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@DataDeNascimento", funcionario.DataDeNascimento);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                string Query = "SELECT IdFuncionario, Nome, Sobrenome, DataDeNascimento FROM Funcionarios";
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"])
                            ,Nome = sdr["Nome"].ToString()
                            ,Sobrenome = sdr["Sobrenome"].ToString()
                            ,DataDeNascimento = Convert.ToDateTime(sdr["DataDeNascimento"])
                        };
                        funcionarios.Add(funcionario);
                    }
                }

            }
            return funcionarios;
        }

        public List<FuncionarioDomain> ListarNomesCompletos()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                //string Query = "SELECT IdFuncionario, Nome, Sobrenome , DataDeNascimento FROM Funcionarios";
                string Query = "SELECT IdFuncionario, Nome, Sobrenome , DataDeNascimento FROM Funcionarios";
                con.Open();
                SqlDataReader sdr;
                //cmd.Parameters.AddWithValue("@NomeCompleto", sdr["Nome"].ToString() + " " + sdr["Sobrenome"].ToString());
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    
                    while (sdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"])
                            ,NomeCompleto = sdr["Nome"].ToString() + " " + sdr["Sobrenome"].ToString()
                            ,DataDeNascimento = Convert.ToDateTime(sdr["DataDeNascimento"])
                        };
                        
                        funcionarios.Add(funcionario);
                    }
                }

            }
            return funcionarios;
        }

        public void Deletar(int id)
        {
            string Query = "DELETE FROM Funcionarios WHERE IdFuncionario = @IdFuncionario";
                using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdFuncionario", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(FuncionarioDomain funcionario, int id)
        {
            string Query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome, DataDeNascimento = @DataDeNascimento WHERE IdFuncionario = @IdFuncionario";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdFuncionario", id);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@DataDeNascimento", funcionario.DataDeNascimento);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            string Query = "SELECT IdFuncionario, Nome, Sobrenome, DataDeNascimento FROM Funcionarios WHERE IdFuncionario = @IdFuncionario";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdFuncionario", id);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FuncionarioDomain funcionario = new FuncionarioDomain
                            {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"])
                                ,
                                Nome = sdr["Nome"].ToString()
                                ,
                                Sobrenome = sdr["Sobrenome"].ToString()
                                ,
                                DataDeNascimento = Convert.ToDateTime(sdr["DataDeNascimento"])
                            };
                            return funcionario;
                        }
                    }
                    return null;
                }
            }
        }

        public FuncionarioDomain BuscarPorNome(string nome)
        {
            string Query = "SELECT IdFuncionario, Nome, Sobrenome, DataDeNascimento FROM Funcionarios WHERE Nome = @Nome";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FuncionarioDomain funcionario = new FuncionarioDomain
                            {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"])
                                ,
                                Nome = sdr["Nome"].ToString()
                                ,
                                Sobrenome = sdr["Sobrenome"].ToString()
                                ,
                                DataDeNascimento = Convert.ToDateTime(sdr["DataDeNascimento"])
                            };
                            return funcionario;
                        }
                    }
                    return null;
                }
            }
        }


    }
}
