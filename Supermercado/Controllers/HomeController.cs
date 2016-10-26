using Supermercado.AcessoDados;
using Supermercado.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Supermercado.Controllers {
    public class HomeController : Controller {

        private readonly BancoContexto _db = new BancoContexto();

        public ActionResult Index() {
            return View();
        }

        public ActionResult ListarPorGenero(int id)
        {

            ConteudoPorGenero conteudoPorGenero = new ConteudoPorGenero();

            List<Livro> livros;
            List<Game> games;
            List<Filme> filmes;
           

            if (id != 0)
            {
                Genero genero = _db.Generos.FirstOrDefault(g => g.Id == id);

                if (genero != null) conteudoPorGenero.Genero = genero.Nome;
                livros = _db.Livros.Where(l => l.GeneroId == id).ToList();
                games = _db.Games.Where(g => g.GeneroId == id).ToList();
                filmes = _db.Filmes.Where(f => f.GeneroId == id).ToList();
                

            }
            else
            {
                livros = _db.Livros.ToList();
                games = _db.Games.ToList();
                filmes = _db.Filmes.ToList();
            }

            conteudoPorGenero.Livros = livros;
            conteudoPorGenero.Games = games;
            conteudoPorGenero.Filmes = filmes;
            

            return PartialView(conteudoPorGenero);
        }
    }
}