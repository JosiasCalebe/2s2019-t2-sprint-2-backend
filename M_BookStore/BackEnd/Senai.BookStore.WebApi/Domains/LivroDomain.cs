using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Domains
{
    public class LivroDomain
    {
        public int IdLivro { get; set; }
        [Required(ErrorMessage = "A Descrição é obrigatória")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O Id do Autor é obrigatório")]
        public int IdAutor { get; set; }
        [Required(ErrorMessage = "O Id do Genero é obrigatório")]
        public int IdGenero { get; set; }
        public AutorDomain Autor { get; set; }
        public GeneroDomain Genero { get; set; }
    }
}
