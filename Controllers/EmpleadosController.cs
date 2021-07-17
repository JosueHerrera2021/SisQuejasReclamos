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
    public class EmpleadosController : Controller
    {
        private QuejasDBEntities1 db = new QuejasDBEntities1();

        // GET: Empleados
        [Authorize(Roles = "General")]
        public async Task<ActionResult> Index()
        {
            var empleados = db.Empleados.Include(e => e.Departamento).Include(e => e.EstadoPersonal);
            return View(await empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre");
            ViewBag.Id_EstadoP = new SelectList(db.EstadoPersonals, "Id_EstadoP", "Descripcion");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Empleado,Nombre,Id_Departamento,Id_EstadoP")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", empleado.Id_Departamento);
            ViewBag.Id_EstadoP = new SelectList(db.EstadoPersonals, "Id_EstadoP", "Descripcion", empleado.Id_EstadoP);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", empleado.Id_Departamento);
            ViewBag.Id_EstadoP = new SelectList(db.EstadoPersonals, "Id_EstadoP", "Descripcion", empleado.Id_EstadoP);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_Empleado,Nombre,Id_Departamento,Id_EstadoP")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Departamento = new SelectList(db.Departamentos, "Id_Departamento", "Nombre", empleado.Id_Departamento);
            ViewBag.Id_EstadoP = new SelectList(db.EstadoPersonals, "Id_EstadoP", "Descripcion", empleado.Id_EstadoP);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empleado empleado = await db.Empleados.FindAsync(id);
            db.Empleados.Remove(empleado);
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
