using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class ClassificacoesIndicativas
    {
        public ClassificacoesIndicativas()
        {
            Lancamentos = new HashSet<Lancamentos>();
        }

        public int IdClassificacaoIndicativa { get; set; }
        public byte ClassificacaoIndicativa { get; set; }
        public string Ci { get; set; }

        public ICollection<Lancamentos> Lancamentos { get; set; }
    }
}
