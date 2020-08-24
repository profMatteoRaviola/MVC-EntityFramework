using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework;
using MVC_EntityFramework.Models;

namespace MVC_EntityFramework.Controllers
{
    public class PianteController : Controller
    {
        private readonly Context _context;

        public PianteController(Context context)
        {
            _context = context;
        }

        // GET: Piante
        public async Task<IActionResult> Index()
        {
            return View(await _context.Piante.ToListAsync());
        }

        // GET: Piante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pianta = await _context.Piante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pianta == null)
            {
                return NotFound();
            }

            return View(pianta);
        }

        // GET: Piante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Piante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Prezzo")] Pianta pianta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pianta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pianta);
        }

        // GET: Piante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pianta = await _context.Piante.FindAsync(id);
            if (pianta == null)
            {
                return NotFound();
            }
            return View(pianta);
        }

        // POST: Piante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Prezzo")] Pianta pianta)
        {
            if (id != pianta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pianta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiantaExists(pianta.Id))
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
            return View(pianta);
        }

        // GET: Piante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pianta = await _context.Piante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pianta == null)
            {
                return NotFound();
            }

            return View(pianta);
        }

        // POST: Piante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pianta = await _context.Piante.FindAsync(id);
            _context.Piante.Remove(pianta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PiantaExists(int id)
        {
            return _context.Piante.Any(e => e.Id == id);
        }
    }
}
