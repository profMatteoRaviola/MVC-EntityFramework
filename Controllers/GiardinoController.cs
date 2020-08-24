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
    public class GiardinoController : Controller
    {
        private readonly Context _context;

        public GiardinoController(Context context)
        {
            _context = context;
        }

        // GET: Giardino
        public async Task<IActionResult> Index()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            var context = _context.Giardino.Include(g => g.Pianta);
            return View(await context.ToListAsync());
        }

        // GET: Giardino/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giardino = await _context.Giardino
                .Include(g => g.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (giardino == null)
            {
                return NotFound();
            }

            return View(giardino);
        }

        // GET: Giardino/Create
        public IActionResult Create()
        {
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome");
            return View();
        }

        // POST: Giardino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PiantaID,Stagione")] Giardino giardino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giardino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", giardino.PiantaID);
            return View(giardino);
        }

        // GET: Giardino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giardino = await _context.Giardino.FindAsync(id);
            if (giardino == null)
            {
                return NotFound();
            }
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", giardino.PiantaID);
            return View(giardino);
        }

        // POST: Giardino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PiantaID,Stagione")] Giardino giardino)
        {
            if (id != giardino.PiantaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giardino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiardinoExists(giardino.PiantaID))
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
            ViewData["PiantaID"] = new SelectList(_context.Piante, "Id", "Nome", giardino.PiantaID);
            return View(giardino);
        }

        // GET: Giardino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giardino = await _context.Giardino
                .Include(g => g.Pianta)
                .FirstOrDefaultAsync(m => m.PiantaID == id);
            if (giardino == null)
            {
                return NotFound();
            }

            return View(giardino);
        }

        // POST: Giardino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giardino = await _context.Giardino.FindAsync(id);
            _context.Giardino.Remove(giardino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiardinoExists(int id)
        {
            return _context.Giardino.Any(e => e.PiantaID == id);
        }
    }
}
