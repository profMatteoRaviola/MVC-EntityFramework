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
    public class FioriController : Controller
    {
        private readonly Context _context;

        public FioriController(Context context)
        {
            _context = context;
        }

        // GET: Fiori
        public async Task<IActionResult> Index()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            var context = _context.Fiore.Include(f => f.Pianta);
            return View(await context.ToListAsync());
        }

        // GET: Fiori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fiore = await _context.Fiore
                .Include(f => f.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (fiore == null)
            {
                return NotFound();
            }

            return View(fiore);
        }

        // GET: Fiori/Create
        public IActionResult Create()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            return View();
        }

        // POST: Fiori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PiantaID,Colore")] Fiore fiore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fiore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", fiore.PiantaID);
            return View(fiore);
        }

        // GET: Fiori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fiore = await _context.Fiore.FindAsync(id);
            if (fiore == null)
            {
                return NotFound();
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", fiore.PiantaID);
            return View(fiore);
        }

        // POST: Fiori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PiantaID,Colore")] Fiore fiore)
        {
            if (id != fiore.PiantaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fiore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FioreExists(fiore.PiantaID))
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
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", fiore.PiantaID);
            return View(fiore);
        }

        // GET: Fiori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fiore = await _context.Fiore
                .Include(f => f.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (fiore == null)
            {
                return NotFound();
            }

            return View(fiore);
        }

        // POST: Fiori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fiore = await _context.Fiore.FindAsync(id);
            _context.Fiore.Remove(fiore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FioreExists(int id)
        {
            return _context.Fiore.Any(e => e.PiantaID == id);
        }
    }
}
