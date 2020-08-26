using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework.Models;
using SQLitePCL;

namespace MVC_EntityFramework.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clienti_listController : ControllerBase
    {
        private readonly Context _context;

        public Clienti_listController(Context context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienti() //risponde al verbo http GET
        {
            return await _context.Clienti.ToListAsync(); //viene fatta la serilizzazione degli oggetti in json
        }
    }
}
