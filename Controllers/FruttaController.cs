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
    public class FruttaController : Controller
    {
        private readonly Context _context;

        public FruttaController(Context context)
        {
            _context = context;
        }

        // GET: Frutta
        public async Task<IActionResult> Index()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            var context = _context.Frutta.Include(f => f.Pianta);
            return View(await context.ToListAsync());
        }

        // GET: Frutta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frutta = await _context.Frutta
                .Include(f => f.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (frutta == null)
            {
                return NotFound();
            }

            return View(frutta);
        }

        // GET: Frutta/Create
        public IActionResult Create()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            return View();
        }

        // POST: Frutta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PiantaID,Produttivita")] Frutta frutta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(frutta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", frutta.PiantaID);
            return View(frutta);
        }

        // GET: Frutta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frutta = await _context.Frutta.FindAsync(id);
            if (frutta == null)
            {
                return NotFound();
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", frutta.PiantaID);
            return View(frutta);
        }

        // POST: Frutta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PiantaID,Produttivita")] Frutta frutta)
        {
            if (id != frutta.PiantaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(frutta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FruttaExists(frutta.PiantaID))
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
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", frutta.PiantaID);
            return View(frutta);
        }

        // GET: Frutta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frutta = await _context.Frutta
                .Include(f => f.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (frutta == null)
            {
                return NotFound();
            }

            return View(frutta);
        }

        // POST: Frutta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var frutta = await _context.Frutta.FindAsync(id);
            _context.Frutta.Remove(frutta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FruttaExists(int id)
        {
            return _context.Frutta.Any(e => e.PiantaID == id);
        }
    }
}
