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
    public class EstadoQRsController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: EstadoQRs
        [Authorize(Roles ="General, Call Center")]
        public async Task<ActionResult> Index()
        {
            return View(await db.EstadoQRs.ToListAsync());
        }

        // GET: EstadoQRs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoQR estadoQR = await db.EstadoQRs.FindAsync(id);
            if (estadoQR == null)
            {
                return HttpNotFound();
            }
            return View(estadoQR);
        }

        // GET: EstadoQRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoQRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_EstadoQR,Descripcion")] EstadoQR estadoQR)
        {
            if (ModelState.IsValid)
            {
                db.EstadoQRs.Add(estadoQR);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(estadoQR);
        }

        // GET: EstadoQRs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoQR estadoQR = await db.EstadoQRs.FindAsync(id);
            if (estadoQR == null)
            {
                return HttpNotFound();
            }
            return View(estadoQR);
        }

        // POST: EstadoQRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_EstadoQR,Descripcion")] EstadoQR estadoQR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoQR).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estadoQR);
        }

        // GET: EstadoQRs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoQR estadoQR = await db.EstadoQRs.FindAsync(id);
            if (estadoQR == null)
            {
                return HttpNotFound();
            }
            return View(estadoQR);
        }

        // POST: EstadoQRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EstadoQR estadoQR = await db.EstadoQRs.FindAsync(id);
            db.EstadoQRs.Remove(estadoQR);
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
