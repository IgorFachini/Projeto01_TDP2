using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermercado.AcessoDados;
using Supermercado.Models;
using System.Linq.Dynamic;

namespace Supermercado.Controllers
{
    public class MangasController : Controller
    {
        private BancoContexto _db = new BancoContexto();

        // GET: Mangas
        public ActionResult Index()
        {
            //var mangas = _db.Mangas.Include(m => m.Genero).Include(m => m.Tipo);
            //return View(mangas.ToList());
            return View();
        }

        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var mangas = _db.Mangas.Include(l => l.Genero);

            var total = mangas.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                int ano;
                int.TryParse(searchPhrase, out ano);

                decimal valor;
                decimal.TryParse(searchPhrase, out valor);
                mangas = mangas.Where("Titulo.Contains(@0) OR Autor.Contains(@0) OR Ano == @1 OR Valor = @2", searchPhrase, ano, valor);
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var mangasPaginados = mangas.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new
            {
                rows = mangasPaginados.ToList(),
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

            Manga manga = _db.Mangas.Include(l => l.Genero).FirstOrDefault(l => l.Id == id.Value);
            // manga = _db.Mangas.Include(l => l.Tipo).FirstOrDefault(l => l.Id == id.Value);

            if (manga == null)
            {
                return HttpNotFound();
            }

            return PartialView(manga);
        }
        // GET: Mangas/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Manga manga)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Manga"));
            manga.Tipo = tipo;
            manga.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Mangas.Add(manga);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "Manga cadastrado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }

        }

        // GET: Mangas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = _db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome", manga.GeneroId);
            return PartialView(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Manga manga)
        {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Manga"));
            manga.Tipo = tipo;
            manga.TipoId = tipo.Id;
            if (ModelState.IsValid)
            {
                _db.Entry(manga).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "Manga editado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Mangas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = _db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            return PartialView(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Manga manga = _db.Mangas.Find(id);

                _db.Mangas.Remove(manga);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "Manga excluído com sucesso" });
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
