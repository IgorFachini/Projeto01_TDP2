using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado.Models {
    public class Genero {
        public int Id { get; set; }
        [Display(Name = "Gênero")]
        public String Nome { get; set; }
    }
}