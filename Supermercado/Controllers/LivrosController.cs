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
    public class LivrosController : Controller
    {
        private readonly BancoContexto _db = new BancoContexto();

        // GET: Livros
        public ActionResult Index()
        {        
            return View();
        }
        
        public JsonResult Listar(string searchPhrase, int current = 1, int rowCount = 5) {
            var chave = Request.Form.AllKeys.First(k => k.StartsWith("sort"));
            var ordenacao = Request[chave];
            var campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var livros = _db.Livros.Include(l => l.Genero);

            var total = livros.Count();

            if (!string.IsNullOrWhiteSpace(searchPhrase)) {
                int ano;
                int.TryParse(searchPhrase, out ano);

                decimal valor;
                decimal.TryParse(searchPhrase, out valor);
                livros = livros.Where("Titulo.Contains(@0) OR Autor.Contains(@0) OR AnoEdicao == @1 OR Valor = @2", searchPhrase, ano, valor);
            }

            string campoOrdenacao = $"{campo} {ordenacao}";

            var livrosPaginados = livros.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

            return Json(new {
                rows = livrosPaginados.ToList(), current, rowCount, total
            }
            , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Livro livro = _db.Livros.Include(l => l.Genero).FirstOrDefault(l => l.Id == id.Value);
                 // livro = db.Livros.Include(l => l.Tipo).FirstOrDefault(l => l.Id == id.Value);

            if (livro == null) {
                return HttpNotFound();
            }

            return PartialView(livro);
        }
        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Livro livro) {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Livro"));
            livro.Tipo = tipo;
            livro.TipoId = tipo.Id;
            if (ModelState.IsValid) {
                _db.Livros.Add(livro);
                _db.SaveChanges();
                return Json(new { resultado = true, message = "Livro cadastrado com sucesso" });
            } else {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }

        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = _db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(_db.Generos, "Id", "Nome", livro.GeneroId);
            return PartialView(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Livro livro) {
            var tipos = _db.Tipos.ToList();
            var tipo = tipos.Find(tipo1 => tipo1.Nome.Contains("Livro"));
            livro.Tipo = tipo;
            livro.TipoId = tipo.Id;
            if (ModelState.IsValid) {
                _db.Entry(livro).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new { resultado = true, message = "Livro editado com sucesso" });
            } else {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);

                return Json(new { resultado = false, message = erros });
            }
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = _db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return PartialView(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id) {
            try {
                Livro livro = _db.Livros.Find(id);

                _db.Livros.Remove(livro);

                _db.SaveChanges();

                return Json(new { resultado = true, message = "Livro excluído com sucesso" });
            } catch (Exception e) {
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
