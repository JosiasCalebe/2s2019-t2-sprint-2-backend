using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.ViewModels
{
    public class LancamentoViewModel
    {
        public int IdLancamento { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Categoria { get; set; }
        public string Plataforma { get; set; }
        public string ClassificacaoIndicativa { get; set; }
        public DateTime DataDeLancamento { get; set; }
        public string TipoDeMidia { get; set; }
        public TimeSpan? TempoDeDuracao { get; set; }
        public int? Episodios { get; set; }
    }
}
