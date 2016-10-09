using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermercado.Models {
    public class Filme {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Diretor { get; set; }
        public int AnoLancamento { get; set; }
        public decimal Valor { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
    }
}