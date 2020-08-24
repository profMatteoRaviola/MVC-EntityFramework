using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Frutta
    {
        [Key]
        [Required]
        public int PiantaID { get; set; } //chiave esterna della pianta che è primaria di  una pianta da Frutta

        [Required(ErrorMessage = "Attributo 'Produttività' richiesto")]
        //[RegularExpression("[A-Z][a-z]{1,9}", ErrorMessage = "Formato colore errato")]
        public float Produttivita { get; set; } //kg di frutta media prodotta all'anno

        public Pianta Pianta { get; set; }
    }
}
