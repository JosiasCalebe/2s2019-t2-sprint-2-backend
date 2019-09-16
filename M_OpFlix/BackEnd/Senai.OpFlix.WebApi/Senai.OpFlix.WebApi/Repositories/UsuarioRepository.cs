using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.Utils;
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
                usuario.Senha = HasherDeSenhas.Hash(usuario.Senha);
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
            }
        }

        public List<Usuarios> Listar()
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                var lista = ctx.Usuarios.ToList();
                foreach (var item in lista) item.Senha = null;
                return lista;
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
                cmd.Parameters.AddWithValue("@Senha", HasherDeSenhas.Hash(usuario.Senha));
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
                a.Senha = HasherDeSenhas.Hash(usuario.Senha);
                if(usuario.ImagemUsuario != null) a.ImagemUsuario = usuario.ImagemUsuario;
                ctx.Usuarios.Update(a);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Usuarios usuario = ctx.Usuarios.Find(id);
                ctx.Usuarios.Remove(usuario);
                ctx.SaveChanges();
            }
        }

        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                Usuarios Usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email);
                if (HasherDeSenhas.Verificar(login.Senha, Usuario.Senha))
                {
                    return Usuario;
                }
                return null;
            }
        }

        public Usuarios BuscarPorId(int id)
        {
            using (OpFlixContext ctx = new OpFlixContext())
            {
                return ctx.Usuarios.Find(id);
            }
        }
    }
}
