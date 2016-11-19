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
    
    public class GenerosController : Controller
    {
        private readonly BancoContexto _db = new BancoContexto();

        // GET: Generos
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var generos = _db.Generos.ToList();

            var total = generos.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {            
                generos = generos.FindAll(g => g.Nome.ToLower().Contains(searchPhrase.ToLower()));
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var generosPaginados = generos.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new
            {
                rows = generosPaginados.ToList(),
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
            Genero genero = _db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return PartialView(genero);
        }

      

        // GET: Generos/Create
        public ActionResult Create()
        {
            return PartialView();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Genero genero)
        {
            if (ModelState.IsValid)
            {
                _db.Generos.Add(genero);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "Genero cadastrado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Generos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = _db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return PartialView(genero);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Genero genero)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(genero).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "Genero editado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Generos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = _db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return PartialView(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Genero genero = _db.Generos.Find(id);

                _db.Generos.Remove(genero);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "Genero excluído com sucesso" });
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
