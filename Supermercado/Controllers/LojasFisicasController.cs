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
    public class LojasFisicasController : Controller
    {
        private readonly BancoContexto _db = new BancoContexto();

        // GET: LojaFisicas
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5)
        {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var lojasFisicas = _db.LojasFisicas.ToList();

            var total = lojasFisicas.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                lojasFisicas = lojasFisicas.FindAll(g => g.Estado.ToLower().Contains(searchPhrase.ToLower()));
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var lojasFisicasPaginados = lojasFisicas.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new
            {
                rows = lojasFisicasPaginados.ToList(),
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
            LojaFisica lojaFisica = _db.LojasFisicas.Find(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return PartialView(lojaFisica);
        }

        public PartialViewResult Listar2Listar()
        {
            return PartialView(_db.LojasFisicas.ToList());
        }


        // GET: LojaFisicas/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(LojaFisica lojaFisica)
        {
            if (ModelState.IsValid)
            {
                _db.LojasFisicas.Add(lojaFisica);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "LojaFisica cadastrado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: LojaFisicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LojaFisica lojaFisica = _db.LojasFisicas.Find(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return PartialView(lojaFisica);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(LojaFisica lojaFisica)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(lojaFisica).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "LojaFisica editado com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: LojaFisicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LojaFisica lojaFisica = _db.LojasFisicas.Find(id);
            if (lojaFisica == null)
            {
                return HttpNotFound();
            }
            return PartialView(lojaFisica);
        }

        // POST: LojaFisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                LojaFisica lojaFisica = _db.LojasFisicas.Find(id);

                _db.LojasFisicas.Remove(lojaFisica);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "LojaFisica excluído com sucesso" });
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
