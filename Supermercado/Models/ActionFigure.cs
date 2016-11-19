using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado.Models
{
    public class ActionFigure
    {
        public int Id { get; set; }
        public string Figura { get; set; }
        public string Fabricante { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        [Display(Name = "Tipo")]
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }




    }
}