using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Supermercado.AcessoDados;
using Supermercado.Models;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System;

namespace Supermercado.Controllers
{
    public class FilmesController : Controller
    {
        private readonly BancoContexto _db = new BancoContexto();

        // GET: Filme
        public ActionResult Index()
        {
            // var filmes = _db.Filme.Include(g => g.Genero);
            // return View(filmes.ToList());
            return View();
        }

        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var filmes = _db.Filmes.Include(l => l.Genero);

            var total = filmes.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                int ano;
                int.TryParse(searchPhrase, out ano);

                decimal valor;
                decimal.TryParse(searchPhrase, out valor);
                filmes = filmes.Where("Titulo.Contains(@0) OR Diretor.Contains(@0) OR AnoLancamento == @1 OR Valor = @2", searchPhrase, ano, valor);
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var filmesPaginados = filmes.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new
            {
                rows = filmesPaginados.ToList(),
                current,
                rowCount,
                total
            }
            , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Filme game = _db.Filmes.Include(l => l.Genero).FirstOrDefault(l => l.Id == id.Value);
            // game = db.Filme.Include(l => l.Tipo).FirstOrDefault(l => l.Id == id.Value);

            if (game == null)
            {
                return HttpNotFound();
            }

            return PartialView(game);
        }
        // GET: Filme/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Filme game)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Filme"));
            game.Tipo = tipo;
            game.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Filmes.Add(game);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "Filme cadastrado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }

        }

        // GET: Filme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme game = _db.Filmes.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome", game.GeneroId);
            return PartialView(game);
        }

        // POST: Filme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Filme game)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Filme"));
            game.Tipo = tipo;
            game.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Entry(game).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "Filme editado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Filme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme game = _db.Filmes.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return PartialView(game);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Filme game = _db.Filmes.Find(id);

                _db.Filmes.Remove(game);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "Filme excluído com sucesso" });
            }
            catch (Exception e)
            {
                return Json(new { resultado = false, message = e.Message });
            }


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
