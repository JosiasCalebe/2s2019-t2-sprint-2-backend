using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NomeDeUsuario { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Tipo { get; set; }
        public string ImagemUsuario { get; set; }
        public List<Favoritos> Favoritos { get; set; }
    }
}
