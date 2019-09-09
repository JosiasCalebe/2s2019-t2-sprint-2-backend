using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Plataformas
    {
        public Plataformas()
        {
            Lancamentos = new HashSet<Lancamentos>();
        }

        public int IdPlataforma { get; set; }
        public string Plataforma { get; set; }

        public ICollection<Lancamentos> Lancamentos { get; set; }
    }
}
