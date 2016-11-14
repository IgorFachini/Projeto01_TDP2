﻿using System.ComponentModel.DataAnnotations;

namespace Supermercado.Models {
    public class Filme {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Diretor { get; set; }
        [Display(Name = "Ano do Lançamento")]
        public int AnoLancamento { get; set; }
        public decimal Valor { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }


    }
}