using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Domains
{
    public class FilmeDomain
    {
        public int IdFilme { get; set; }
        [Required(ErrorMessage = "O Titulo é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O Genero é obrigatório")]
        public int IdGenero { get; set; }
        public GeneroDomain Genero { get; set; }
    }
}
