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
    public class ItemVendaController : Controller
    {
        private CRUDEEntities db = new CRUDEEntities();

        private void atualizaTotalVenda(int idVenda)
        {
            Venda venda = db.Venda.Find(idVenda);
            venda.valor = db.ItemVenda.Where(i => i.idVenda == idVenda).Sum(i => (int?)i.valor) ?? 0;
            db.Entry(venda).State = EntityState.Modified;
            db.SaveChanges();
        }

        public JsonResult GetItensVenda(int? id)
        {
            double valor;
            List<Object> listJsonItemVenda = new List<Object>();

            foreach (ItemVenda item in db.ItemVenda.Where(i => i.idVenda == id).ToList())
            {
                valor = (double)item.valor;
                listJsonItemVenda.Add(new
                {
                    idItemVenda = item.idItemVenda,
                    idVenda = item.idVenda,
                    idProduto = item.idProduto,
                    produtoNome = item.Produto.nome,
                    qtd = item.qtd,
                    valor = valor.ToString("c2"),
                    valorFloat = valor,
                });
            }

            return Json(listJsonItemVenda, JsonRequestBehavior.AllowGet);
        }

        // GET: ItemVenda
        public ActionResult ItensVenda()
        {
            var itemVenda = db.ItemVenda.Include(i => i.Produto).Include(i => i.Venda);
            return View(itemVenda.ToList());
        }

        // GET: ItemVenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = db.ItemVenda.Find(id);
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemVenda);
        }

        // GET: ItemVenda/Create
        public ActionResult Create()
        {
            ViewBag.idProduto = new SelectList(db.Produto, "idProduto", "nome");
            ViewBag.idVenda = new SelectList(db.Venda, "idVenda", "idVenda");
            return View();
        }

        // POST: ItemVenda/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idItemVenda,idVenda,idProduto,qtd,valor")] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                db.ItemVenda.Add(itemVenda);
                db.SaveChanges();
                atualizaTotalVenda(itemVenda.idVenda);
                //return RedirectToAction("Index");
                return Redirect("/ItemVenda/ItensVenda?idVenda=" + itemVenda.idVenda);
            }

            ViewBag.idProduto = new SelectList(db.Produto, "idProduto", "nome", itemVenda.idProduto);
            ViewBag.idVenda = new SelectList(db.Venda, "idVenda", "idVenda", itemVenda.idVenda);
            return View(itemVenda);
        }

        // GET: ItemVenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = db.ItemVenda.Find(id);
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProduto = new SelectList(db.Produto, "idProduto", "nome", itemVenda.idProduto);
            ViewBag.idVenda = new SelectList(db.Venda, "idVenda", "idVenda", itemVenda.idVenda);
            return View(itemVenda);
        }

        // POST: ItemVenda/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idItemVenda,idVenda,idProduto,qtd,valor")] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemVenda).State = EntityState.Modified;
                db.SaveChanges();
                atualizaTotalVenda(itemVenda.idVenda);               
                //return RedirectToAction("Index");
                return Redirect("/ItemVenda/ItensVenda?idVenda=" + itemVenda.idVenda);
            }
            ViewBag.idProduto = new SelectList(db.Produto, "idProduto", "nome", itemVenda.idProduto);
            ViewBag.idVenda = new SelectList(db.Venda, "idVenda", "idVenda", itemVenda.idVenda);
            return View(itemVenda);
        }

        // GET: ItemVenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = db.ItemVenda.Find(id);
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemVenda);
        }

        // POST: ItemVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemVenda itemVenda = db.ItemVenda.Find(id);
            db.ItemVenda.Remove(itemVenda);
            db.SaveChanges();
            atualizaTotalVenda(itemVenda.idVenda);
            //return RedirectToAction("Index");
            return Redirect("/ItemVenda/ItensVenda?idVenda=" + itemVenda.idVenda);
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
