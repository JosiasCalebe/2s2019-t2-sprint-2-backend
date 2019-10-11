using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Reviews
    {
        public int? IdUsuario { get; set; }
        public int? IdLancamento { get; set; }
        public string Review { get; set; }
        public int Nota { get; set; }
        public int IdReview { get; set; }

        public Lancamentos IdLancamentoNavigation { get; set; }
        public Usuarios IdUsuarioNavigation { get; set; }
    }
}
