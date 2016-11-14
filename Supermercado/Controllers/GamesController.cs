using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Supermercado.AcessoDados;
using Supermercado.Models;
using System.Linq.Dynamic;

namespace Supermercado.Controllers
{
    public class GamesController : Controller
    {
        private readonly BancoContexto _db  = new BancoContexto();

        // GET: Games
        public ActionResult Index()
        {
           // var games = _db.Games.Include(g => g.Genero);
           // return View(games.ToList());
            return View();
        }

        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var games = _db.Games.Include(l => l.Genero);

            var total = games.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                int ano;
                int.TryParse(searchPhrase, out ano);

                decimal valor;
                decimal.TryParse(searchPhrase, out valor);
                games = games.Where("Titulo.Contains(@0) OR Plataforma.Contains(@0) OR AnoLancamento == @1 OR Valor = @2", searchPhrase, ano, valor);
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var gamesPaginados = games.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new
            {
                rows = gamesPaginados.ToList(),
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

            Game game = _db.Games.Include(l => l.Genero).FirstOrDefault(l => l.Id == id.Value);
            // game = db.Games.Include(l => l.Tipo).FirstOrDefault(l => l.Id == id.Value);

            if (game == null)
            {
                return HttpNotFound();
            }

            return PartialView(game);
        }
        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Game game)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Game"));
            game.Tipo = tipo;
            game.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Games.Add(game);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "Game cadastrado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }

        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome", game.GeneroId);
            return PartialView(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Game game)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Game"));
            game.Tipo = tipo;
            game.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Entry(game).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "Game editado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return PartialView(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Game game = _db.Games.Find(id);

                _db.Games.Remove(game);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "Game excluído com sucesso" });
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
