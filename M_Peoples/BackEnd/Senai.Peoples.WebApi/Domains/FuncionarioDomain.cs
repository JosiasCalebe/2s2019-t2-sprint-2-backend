using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Domains
{
    public class FuncionarioDomain
    {
        public int IdFuncionario { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatório")]
        public DateTime DataDeNascimento { get; set; }
    }
}
