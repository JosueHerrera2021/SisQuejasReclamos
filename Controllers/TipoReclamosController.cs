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
    public class TipoReclamosController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: TipoReclamos
        [Authorize(Roles = "General, Call Center")]
        public async Task<ActionResult> Index()
        {
            var tipoReclamoes = db.TipoReclamoes.Include(t => t.EstadoG);
            return View(await tipoReclamoes.ToListAsync());
        }

        // GET: TipoReclamos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReclamo tipoReclamo = await db.TipoReclamoes.FindAsync(id);
            if (tipoReclamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoReclamo);
        }

        // GET: TipoReclamos/Create
        public ActionResult Create()
        {
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion");
            return View();
        }

        // POST: TipoReclamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_TipoReclamo,Tipo,Descripcion,Id_EstadoG")] TipoReclamo tipoReclamo)
        {
            if (ModelState.IsValid)
            {
                db.TipoReclamoes.Add(tipoReclamo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoReclamo.Id_EstadoG);
            return View(tipoReclamo);
        }

        // GET: TipoReclamos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReclamo tipoReclamo = await db.TipoReclamoes.FindAsync(id);
            if (tipoReclamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoReclamo.Id_EstadoG);
            return View(tipoReclamo);
        }

        // POST: TipoReclamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_TipoReclamo,Tipo,Descripcion,Id_EstadoG")] TipoReclamo tipoReclamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoReclamo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_EstadoG = new SelectList(db.EstadoGs, "Id_EstadoG", "Descripcion", tipoReclamo.Id_EstadoG);
            return View(tipoReclamo);
        }

        // GET: TipoReclamos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReclamo tipoReclamo = await db.TipoReclamoes.FindAsync(id);
            if (tipoReclamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoReclamo);
        }

        // POST: TipoReclamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoReclamo tipoReclamo = await db.TipoReclamoes.FindAsync(id);
            db.TipoReclamoes.Remove(tipoReclamo);
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
