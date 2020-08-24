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
    public class ClientiController : Controller
    {
        private readonly Context _context;

        public ClientiController(Context context)
        {
            _context = context;
        }

        // GET: ControllerClienti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clienti.ToListAsync()); 
            //restituscie l'elenco dei clienti e lo restituisce come modello alla View
        }

        // GET: ControllerClienti/Details/5... 5 è un id cliente
        public async Task<IActionResult> Details(int? id) //come se fosse select di una categoria ocn un certo id
        {
            if (id == null)
            {
                return NotFound(); //error 404 http
            }

            var cliente = await _context.Clienti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: ControllerClienti/Create
        public IActionResult Create() //l'utente qui inserisce i dati
        {
            return View();
        }

        // POST: ControllerClienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cognome")] Cliente cliente) 
            //model bindingper scongiurare gli attacci CSRF
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); //redirect all'index del controller, che mostra l'elenco delle categorie
                //return redirect("url"), permette di fare redirect ad un url passato in input
            }
            return View(cliente); //se il modello non è valido, torno a chiedere i dati tramite la View
        }

        // GET: ControllerClienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: ControllerClienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) //per gestire il sincronismo delle operazioni
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: ControllerClienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clienti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: ControllerClienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clienti.FindAsync(id);
            _context.Clienti.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clienti.Any(e => e.Id == id);
        }
    }
}
