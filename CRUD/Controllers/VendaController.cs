using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class VendaController : Controller
    {
        private CRUDEEntities db = new CRUDEEntities();

        public JsonResult GetVenda()
        {
            List<Object> listJsonVenda = new List<Object>();

            foreach (Venda venda in db.Venda.ToList())
            {
                listJsonVenda.Add(new
                {
                    idVenda = venda.idVenda,
                    data = venda.data.ToString("dd/MM/yyyy hh:mm"),
                    valor = venda.valor,
                    idCliente = venda.idCliente,
                    clienteNome = venda.Cliente.nome,
                });
            }

            return Json(listJsonVenda, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVendaId(int? id)
        {
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return Json(new {});
            }
            Object vendaJson = new
            {
                    idVenda = venda.idVenda,
                    data = venda.data.ToString("dd/MM/yyyy hh:mm"),
                    valor = venda.valor,
                    idCliente = venda.idCliente,
                    clienteNome = venda.Cliente.nome,
            };
            return Json(vendaJson, JsonRequestBehavior.AllowGet);
        }

        // GET: Venda
        public ActionResult Index()
        {
            var venda = db.Venda.Include(v => v.Cliente);
            return View(venda.ToList());
        }

        // GET: Venda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nome");
            return View();
        }

        // POST: Venda/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVenda,idCliente,data,valor")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                venda.data = DateTime.Now;
                db.Venda.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nome", venda.idCliente);
            return View(venda);
        }

        // GET: Venda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nome", venda.idCliente);
            return View(venda);
        }

        // POST: Venda/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVenda,idCliente,data,valor")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nome", venda.idCliente);
            return View(venda);
        }

        // GET: Venda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Venda.Find(id);
            db.Venda.Remove(venda);
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
