using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Fiore
    {
        [Key]
        [Required]
        public int PiantaID { get; set; }

        [Required(ErrorMessage ="Attributo 'Colore' richiesto")]
        [RegularExpression("[A-Z][a-z]{1,9}", ErrorMessage ="Formato colore errato")]
        public string Colore { get; set; }

        public Pianta Pianta { get; set; }
    }
}
