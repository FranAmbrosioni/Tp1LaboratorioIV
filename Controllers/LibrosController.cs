using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebTp1LaboratorioIV.Models;

namespace WebTp1LaboratorioIV.Controllers
{
    public class LibrosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public LibrosController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            // var appDbContext = _context.Libro.Include(l => l.Autor).Include(l => l.Genero);
            return View(await _context.Libro.Include(l => l.Autor).Include(l => l.Genero).ToListAsync());
            //return View(await appDbContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libro
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["AutorList"] = new SelectList(_context.Autor, "ID", "Apellido");
            ViewData["GeneroList"] = new SelectList(_context.Genero, "ID", "Descripcion");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Resumen,fechaPublicacion,Portada,GeneroId,AutorId")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(env.WebRootPath, "portadas");

                    if (archivoFoto.Length > 0)
                    {
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);
                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            libros.Portada = archivoDestino;
                        };
                    }
                }
                _context.Add(libros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Apellido", libros.AutorId);
            //ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Descripcion", libros.GeneroId);
            return View(libros);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var libros = await _context.Libro.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }


            ViewData["AutorList"] = new SelectList(_context.Autor, "ID", "Apellido", libros.AutorId);
            ViewData["GeneroList"] = new SelectList(_context.Genero, "ID", "Descripcion", libros.GeneroId);
            return View(libros);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Resumen,fechaPublicacion,Portada,GeneroId,AutorId")] Libros libros)
        {
            if (id != libros.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos != null && archivos.Count > 0)
                    {
                        var archivoFoto = archivos[0];
                        var pathDestino = Path.Combine(env.WebRootPath, "portadas");

                        if (archivoFoto.Length > 0)
                        {
                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);


                            if (!string.IsNullOrEmpty(libros.Portada))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, libros.Portada);

                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);


                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                libros.Portada = archivoDestino;
                            }
                        }
                    }
                    _context.Update(libros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosExists(libros.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorList"] = new SelectList(_context.Autor, "ID", "Apellido", libros.AutorId);
            ViewData["GeneroList"] = new SelectList(_context.Genero, "ID", "Descripcion", libros.GeneroId);
            return View(libros);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libro
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libros = await _context.Libro.FindAsync(id);
            _context.Libro.Remove(libros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrosExists(int id)
        {
            return _context.Libro.Any(e => e.ID == id);
        }
    }
}