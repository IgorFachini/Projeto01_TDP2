using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermercado.Models
{
    public class ConteudoPorGenero
    {


        public ConteudoPorGenero()
        {
            Genero = "Todos";
        }

        public string Genero { get; set; }
        public List<Livro> Livros { get; set; }
        public List<Game> Games { get; set; }
        public List<Filme> Filmes { get;set; }

    }
}
