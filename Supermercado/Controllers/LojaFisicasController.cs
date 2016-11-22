using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermercado.Models;

namespace Supermercado.Controllers
{
    public class LojaFisicasController : Controller
    {
        private TDP2_NameDataBaseEntities db = new TDP2_NameDataBaseEntities();

        // GET: LojaFisicas
        public async Task<ActionResult> Index()
        {
            return View(await db.LojaFisica.ToListAsync());
        }

        // GET: LojaFisicas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LojaFisica lojaFisica = await db.LojaFisica.FindAsync(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return View(lojaFisica);
        }

        // GET: LojaFisicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LojaFisicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Estado,Cidade,CEP,Telefone")] LojaFisica lojaFisica)
        {
            if (ModelState.IsValid)
            {
                db.LojaFisica.Add(lojaFisica);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lojaFisica);
        }

        // GET: LojaFisicas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LojaFisica lojaFisica = await db.LojaFisica.FindAsync(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return View(lojaFisica);
        }

        // POST: LojaFisicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Estado,Cidade,CEP,Telefone")] LojaFisica lojaFisica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lojaFisica).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lojaFisica);
        }

        // GET: LojaFisicas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LojaFisica lojaFisica = await db.LojaFisica.FindAsync(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return View(lojaFisica);
        }

        // POST: LojaFisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LojaFisica lojaFisica = await db.LojaFisica.FindAsync(id);
            db.LojaFisica.Remove(lojaFisica);
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
