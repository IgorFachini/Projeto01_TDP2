using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermercado.AcessoDados;
using Supermercado.Models;

namespace Supermercado.Controllers
{
    public class ActionFiguresController : Controller
    {
        private BancoContexto db = new BancoContexto();

        // GET: ActionFigures
        public async Task<ActionResult> Index()
        {
            var actionFigures = db.ActionFigures.Include(a => a.Genero).Include(a => a.Tipo);
            return View(await actionFigures.ToListAsync());
        }

        // GET: ActionFigures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionFigure actionFigure = await db.ActionFigures.FindAsync(id);
            if (actionFigure == null)
            {
                return HttpNotFound();
            }
            return View(actionFigure);
        }

        // GET: ActionFigures/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome");
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome");
            return View();
        }

        // POST: ActionFigures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Figura,Fabricante,Descricao,Valor,GeneroId,TipoId")] ActionFigure actionFigure)
        {
            if (ModelState.IsValid)
            {
                db.ActionFigures.Add(actionFigure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", actionFigure.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", actionFigure.TipoId);
            return View(actionFigure);
        }

        // GET: ActionFigures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionFigure actionFigure = await db.ActionFigures.FindAsync(id);
            if (actionFigure == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", actionFigure.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", actionFigure.TipoId);
            return View(actionFigure);
        }

        // POST: ActionFigures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Figura,Fabricante,Descricao,Valor,GeneroId,TipoId")] ActionFigure actionFigure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actionFigure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", actionFigure.GeneroId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", actionFigure.TipoId);
            return View(actionFigure);
        }

        // GET: ActionFigures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionFigure actionFigure = await db.ActionFigures.FindAsync(id);
            if (actionFigure == null)
            {
                return HttpNotFound();
            }
            return View(actionFigure);
        }

        // POST: ActionFigures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActionFigure actionFigure = await db.ActionFigures.FindAsync(id);
            db.ActionFigures.Remove(actionFigure);
            await db.SaveChangesAsync();
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
