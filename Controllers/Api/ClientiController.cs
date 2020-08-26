using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework;
using MVC_EntityFramework.Models;

namespace MVC_EntityFramework.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientiController : ControllerBase
    {
        private readonly Context _context;

        public ClientiController(Context context)
        {
            _context = context;
        }

        // GET: api/Clienti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienti() //ottieni l'elenco dei clienti
        {
            return await _context.Clienti.ToListAsync();
        }

        // GET: api/Clienti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)//ottieni un cliente in particolare
        {
            var cliente = await _context.Clienti.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clienti/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente) //modifica il cliente in input
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clienti
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente) //crea un cliente tramite deserializzazione del json
        {
            _context.Clienti.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente); //richiamo il verbo GET per il cliente
        }

        // DELETE: api/Clienti/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id) //cancella un cliente
        {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clienti.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Clienti.Any(e => e.Id == id);
        }
    }
}
