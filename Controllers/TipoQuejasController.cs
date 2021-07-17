using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisQuejasReclamos.Models;

namespace SisQuejasReclamos.Controllers
{
    public class TipoQuejasController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: TipoQuejas
        [Authorize(Roles = "General, Call Center")]
        public async Task<ActionResult> Index()
        {
            var tipoQuejas = db.TipoQuejas.Include(t => t.EstadoG);
            return View(await tipoQuejas.ToListAsync());
        }

        // GET: TipoQuejas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoQueja tipoQueja = await db.TipoQuejas.FindAsync(id);
            if (tipoQueja == null)
            {
                return HttpNotFound();
            }
            return View(tipoQueja);
        }

        // GET: TipoQuejas/Create
        public ActionResult Create()
        {
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion");
            return View();
        }

        // POST: TipoQuejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_TipoQueja,Tipo,Descripcion,Id_EstadoG")] TipoQueja tipoQueja)
        {
            if (ModelState.IsValid)
            {
                db.TipoQuejas.Add(tipoQueja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoQueja.Id_EstadoG);
            return View(tipoQueja);
        }

        // GET: TipoQuejas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoQueja tipoQueja = await db.TipoQuejas.FindAsync(id);
            if (tipoQueja == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoQueja.Id_EstadoG);
            return View(tipoQueja);
        }

        // POST: TipoQuejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_TipoQueja,Tipo,Descripcion,Id_EstadoG")] TipoQueja tipoQueja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoQueja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoQueja.Id_EstadoG);
            return View(tipoQueja);
        }

        // GET: TipoQuejas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoQueja tipoQueja = await db.TipoQuejas.FindAsync(id);
            if (tipoQueja == null)
            {
                return HttpNotFound();
            }
            return View(tipoQueja);
        }

        // POST: TipoQuejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoQueja tipoQueja = await db.TipoQuejas.FindAsync(id);
            db.TipoQuejas.Remove(tipoQueja);
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
