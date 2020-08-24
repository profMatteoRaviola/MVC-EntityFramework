using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Dettaglio_Ordine
    {
        [Key] [Required] public int Id { get; set; }

        [Required] public int Quantita { get; set; }

        [Required] public int PiantaID { get; set; }
        [Required] public int OrdineID { get; set; }

        public Ordine Ordine { get; set; }
        public Pianta Pianta { get; set; }
    }
}
