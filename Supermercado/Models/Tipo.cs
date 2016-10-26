using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        //[Display(Name = "Tipo")]
        public String Nome { get; set; }

        
    }
}