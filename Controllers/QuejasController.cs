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
    public class QuejasController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: Quejas
        [Authorize(Roles = "General, Call Center")]
        public async Task<ActionResult> Index()
        {
            var quejas = db.Quejas.Include(q => q.Cliente).Include(q => q.Departamento).Include(q => q.Empleado).Include(q => q.Producto).Include(q => q.TipoQueja).Include(q => q.EstadoQR);
            return View(await quejas.ToListAsync());
        }

        // GET: Quejas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = await db.Quejas.FindAsync(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // GET: Quejas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre");
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre");
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre");
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre");
            ViewBag.Id_TipoQueja = new SelectList(db.TipoQuejas, "Id_TipoQueja", "Tipo");
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion");
            return View();
        }

        // POST: Quejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Queja,Id_Cliente,Id_TipoQueja,Id_Producto,Id_Departamento,Id_Empleado,Descripcion,Fecha,Id_EstadoQR")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Quejas.Add(queja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", queja.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", queja.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", queja.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", queja.Id_Producto);
            ViewBag.Id_TipoQueja = new SelectList(db.TipoQuejas, "Id_TipoQueja", "Tipo", queja.Id_TipoQueja);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", queja.Id_EstadoQR);
            return View(queja);
        }

        // GET: Quejas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = await db.Quejas.FindAsync(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", queja.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", queja.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", queja.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", queja.Id_Producto);
            ViewBag.Id_TipoQueja = new SelectList(db.TipoQuejas, "Id_TipoQueja", "Tipo", queja.Id_TipoQueja);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", queja.Id_EstadoQR);
            return View(queja);
        }

        // POST: Quejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Queja,Id_Cliente,Id_TipoQueja,Id_Producto,Id_Departamento,Id_Empleado,Descripcion,Fecha,Id_EstadoQR")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", queja.Id_Cliente);
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", queja.Id_Departamento);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id_Empleado", "Nombre", queja.Id_Empleado);
            ViewBag.Id_Producto = new SelectList(db.Productoes, "Id_Producto", "Nombre", queja.Id_Producto);
            ViewBag.Id_TipoQueja = new SelectList(db.TipoQuejas, "Id_TipoQueja", "Tipo", queja.Id_TipoQueja);
            ViewBag.Id_EstadoQR = new SelectList(db.EstadoQRs, "Id_EstadoQR", "Descripcion", queja.Id_EstadoQR);
            return View(queja);
        }

        // GET: Quejas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queja queja = await db.Quejas.FindAsync(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // POST: Quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Queja queja = await db.Quejas.FindAsync(id);
            db.Quejas.Remove(queja);
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
