using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Supermercado.Models {
    public class Livro {
        
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public string Autor { get; set; }

        [Display(Name = "Ano da Edição")]
        public int AnoEdicao { get; set; }

        public decimal Valor { get; set; }

        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        [Display(Name = "Tipo")]
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }

        


    }
}