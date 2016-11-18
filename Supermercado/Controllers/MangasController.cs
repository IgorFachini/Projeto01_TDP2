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

namespace Supermercado.Controllers
{
    public class MangasController : Controller
    {
        private BancoContexto db = new BancoContexto();

        // GET: Mangas
        public ActionResult Index()
        {
            var mangas = db.Mangas.Include(m => m.Genero).Include(m => m.Tipo);
            return View(mangas.ToList());
        }

        // GET: Mangas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            return View(manga);
        }

        // GET: Mangas/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome");
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome");
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Autor,Ano,Descricao,Valor,GeneroId,TipoId")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                db.Mangas.Add(manga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", manga.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", manga.TipoId);
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", manga.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", manga.TipoId);
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Autor,Ano,Descricao,Valor,GeneroId,TipoId")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", manga.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", manga.TipoId);
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manga manga = db.Mangas.Find(id);
            db.Mangas.Remove(manga);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
