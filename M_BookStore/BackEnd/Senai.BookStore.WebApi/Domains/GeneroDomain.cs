using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Domains
{
    public class GeneroDomain
    {
        public int IdGenero { get; set; }
        [Required(ErrorMessage = "A Descrição é obrigatória")]
        public string Descricao { get; set; }
    }
}
