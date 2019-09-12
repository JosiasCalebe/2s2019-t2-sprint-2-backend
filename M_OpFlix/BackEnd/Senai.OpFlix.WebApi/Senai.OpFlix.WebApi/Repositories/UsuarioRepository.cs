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
    public class UsuarioRepository : IUsuarioRepository
    {
        private string Conexao = "Data Source=.\\SqlExpress; initial catalog=M_OpFlix; User Id=sa;Pwd=132";

        public void CadastrarAdmin(Usuarios usuario)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
            }
        }

        public List<Usuarios> Listar()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Usuarios.ToList();
            }
        }

            public void Cadastrar(Usuarios usuario)
        {
            string Query;
            SqlCommand cmd;
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                if (usuario.ImagemUsuario == null)
                {
                    Query = "INSERT INTO Usuarios(Nome, Email, Senha, NomeDeUsuario, DataDeNascimento) VALUES (@Nome, @Email, @Senha, @NomeDeUsuario, @DataDeNascimento)";
                    cmd = new SqlCommand(Query, con);
                }
                else
                {
                    Query = "INSERT INTO Usuarios(Nome, Email, Senha, NomeDeUsuario, DataDeNascimento,ImagemUsuario) VALUES (@Nome, @Email, @Senha, @NomeDeUsuario, @DataDeNascimento, @ImagemUsuario)";
                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@ImagemUsuario", usuario.ImagemUsuario);

                }
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@NomeDeUsuario", usuario.NomeDeUsuario);
                cmd.Parameters.AddWithValue("@DataDeNascimento", usuario.DataDeNascimento);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(int id, Usuarios usuario)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var a = ctx.Usuarios.Find(id);
                a.Nome = usuario.Nome;
                a.NomeDeUsuario = usuario.NomeDeUsuario;
                a.Email = usuario.Email;
                a.ImagemUsuario = usuario.ImagemUsuario;
                ctx.Usuarios.Update(a);
                ctx.SaveChanges();
            }
        }
        public void Atualizar(string nomeDeUsuario, Usuarios usuario)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var a = ctx.Usuarios.FirstOrDefault(x=> x.NomeDeUsuario == nomeDeUsuario);
                a.Nome = usuario.Nome;
                a.NomeDeUsuario = usuario.NomeDeUsuario;
                a.Email = usuario.Email;
                a.ImagemUsuario = usuario.ImagemUsuario;
                ctx.Usuarios.Update(a);
                ctx.SaveChanges();
            }
        }

        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Usuarios Usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (Usuario == null)
                {
                    return null;
                }
                return Usuario;
            }
        }
    }
}
