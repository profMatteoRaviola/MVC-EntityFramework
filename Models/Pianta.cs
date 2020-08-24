using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Pianta
    {
        public Pianta()
        {
            // Inizializzo una collection vuota.
            Dettaglio_Ordini = new List<Dettaglio_Ordine>();
        }
        [Key] [Required] public int Id { get; set; }

        [Required(ErrorMessage = "L'attributo 'Nome' è richiesto")]
        [RegularExpression("[A-Z][a-z]{1,14}", ErrorMessage = "Formato del nome errato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "L'attributo 'Prezzo' è richiesto")]
        public float Prezzo { get; set; }

        //associazione N:M
        public ICollection<Dettaglio_Ordine> Dettaglio_Ordini { get; }

        //associazioni 1:1
        public Fiore Fiore { get; set; }
        public Frutta Frutta { get; set; }
        public Giardino Giardino { get; set; }
    }
}
