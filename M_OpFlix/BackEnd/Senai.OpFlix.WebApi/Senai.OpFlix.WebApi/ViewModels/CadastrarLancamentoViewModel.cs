using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.ViewModels
{
    public class CadastrarLancamentoViewModel
    {

        public int IdLancamento { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdPlataforma { get; set; }
        public int? IdClassificacaoIndicativa { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public DateTime DataDeLancamento { get; set; }
        public string TipoDeMidia { get; set; }
        public TimeSpan? TempoDeDuracao { get; set; }
        public int Episodios { get; set; }
        public int NotaMedia { get; set; }
        public IFormFile Poster { get; set; }
    }
}
