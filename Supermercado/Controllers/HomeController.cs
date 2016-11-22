using Supermercado.AcessoDados;
using Supermercado.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading;

namespace Supermercado.Controllers {
    [AllowAnonymous]
    public class HomeController : Controller {

        private readonly BancoContexto _db = new BancoContexto();

        public ActionResult Index() {
            return View();
        }

        public ActionResult Ingles() {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            return View("Index");

        }

        public ActionResult Portugues() {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-br");
            return View("Index");
        }


        public ActionResult InglesAbout() {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            return View("About");

        }

        public ActionResult PortuguesAbout() {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-br");
            return View("About");
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

        public PartialViewResult Listar2Listar()
        {
            return PartialView(_db.Generos.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}