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
    public class ReclamosController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: Reclamos
        [Authorize(Roles = "General, Call Center")]
        public async Task<ActionResult> Index()
        {
            var reclamos = db.Reclamos.Include(r => r.Cliente).Include(r => r.Departamento).Include(r => r.Empleado).Include(r => r.Producto).Include(r => r.TipoReclamo).Include(r => r.EstadoQR);
            return View(await reclamos.ToListAsync());
        }

        // GET: Reclamos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamo reclamo = await db.Reclamos.FindAsync(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            return View(reclamo);
        }

        // GET: Reclamos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre");
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre");
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre");
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre");
            ViewBag.Id_TipoReclamo = new SelectList(db.TipoReclamoes, "Id_TipoReclamo", "Tipo");
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion");
            return View();
        }

        // POST: Reclamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Reclamo,Id_Cliente,Id_TipoReclamo,Id_Producto,Id_Departamento,Id_Empleado,Descripcion,Fecha_Ingreso,Id_EstadoQR")] Reclamo reclamo)
        {
            if (ModelState.IsValid)
            {
                db.Reclamos.Add(reclamo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", reclamo.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", reclamo.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", reclamo.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", reclamo.Id_Producto);
            ViewBag.Id_TipoReclamo = new SelectList(db.TipoReclamoes, "Id_TipoReclamo", "Tipo", reclamo.Id_TipoReclamo);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", reclamo.Id_EstadoQR);
            return View(reclamo);
        }

        // GET: Reclamos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamo reclamo = await db.Reclamos.FindAsync(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", reclamo.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", reclamo.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", reclamo.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", reclamo.Id_Producto);
            ViewBag.Id_TipoReclamo = new SelectList(db.TipoReclamoes, "Id_TipoReclamo", "Tipo", reclamo.Id_TipoReclamo);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", reclamo.Id_EstadoQR);
            return View(reclamo);
        }

        // POST: Reclamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Reclamo,Id_Cliente,Id_TipoReclamo,Id_Producto,Id_Departamento,Id_Empleado,Descripcion,Fecha_Ingreso,Id_EstadoQR")] Reclamo reclamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", reclamo.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", reclamo.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", reclamo.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", reclamo.Id_Producto);
            ViewBag.Id_TipoReclamo = new SelectList(db.TipoReclamoes, "Id_TipoReclamo", "Tipo", reclamo.Id_TipoReclamo);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", reclamo.Id_EstadoQR);
            return View(reclamo);
        }

        // GET: Reclamos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamo reclamo = await db.Reclamos.FindAsync(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            return View(reclamo);
        }

        // POST: Reclamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reclamo reclamo = await db.Reclamos.FindAsync(id);
            db.Reclamos.Remove(reclamo);
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
