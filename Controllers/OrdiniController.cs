using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework;
using MVC_EntityFramework.Models;

namespace MVC_EntityFramework.Controllers
{
    public class OrdiniController : Controller
    {
        private readonly Context _context;

        public OrdiniController(Context context)
        {
            _context = context;
        }

        // GET: Ordines
        public async Task<IActionResult> Index()
        {
            
            ViewData["ClienteId1"] = new SelectList(_context.Clienti, "Id", "Nome");
            ViewData["ClienteId2"] = new SelectList(_context.Clienti, "Id", "Cognome");
            //come mettere nella select Nome_Cognome ??
            var context = _context.Ordini.Include(o => o.Cliente);
            return View(await context.ToListAsync());
        }

        // GET: Ordines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordine == null)
            {
                return NotFound();
            }

            return View(ordine);
        }

        // GET: Ordines/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "Id", "Nome"); //la stringa sarà Id Nome Cognome
            return View();
        }

        // POST: Ordines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Data,Mod_pagamento")] Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var nome_cognome = $"{ordine.Cliente.Nome}_{ordine.Cliente.Nome}";
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "Id", "Nome", ordine.ClienteId);
            return View(ordine);
        }

        // GET: Ordines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini.FindAsync(id);
            if (ordine == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "Id", "Cognome", ordine.ClienteId);
            return View(ordine);
        }

        // POST: Ordines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Data,Mod_pagamento")] Ordine ordine)
        {
            if (id != ordine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdineExists(ordine.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "Id", "Cognome", ordine.ClienteId);
            return View(ordine);
        }

        // GET: Ordines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordine == null)
            {
                return NotFound();
            }

            return View(ordine);
        }

        // POST: Ordines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordine = await _context.Ordini.FindAsync(id);
            _context.Ordini.Remove(ordine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdineExists(int id)
        {
            return _context.Ordini.Any(e => e.Id == id);
        }
    }
}
