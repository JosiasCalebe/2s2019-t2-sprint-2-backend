using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Lancamentos
    {
        public Lancamentos()
        {
            Reviews = new HashSet<Reviews>();
        }

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
        public string Poster { get; set; }
        public List<Favoritos> Favoritos { get; set; }

        public Categorias IdCategoriaNavigation { get; set; }
        public ClassificacoesIndicativas IdClassificacaoIndicativaNavigation { get; set; }
        public Plataformas IdPlataformaNavigation { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
    }
}
