using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }
        [StringLength(255, MinimumLength = 6, ErrorMessage = "A senha é obrigatória e deve conter no mínimo 4 caracteres.")]
        public string Senha { get; set; }
    }
}
