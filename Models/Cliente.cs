using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Cliente
    {
        public Cliente()
        {
            // Inizializzo una collection vuota.
            Ordini = new List<Ordine>();
        }
        [Key]
        [Required]
        public int Id { get; set; }

        [Required( ErrorMessage="L'attributo 'Nome' è richiesto")] 
        [RegularExpression("[A-Z][a-z]{1,14}", ErrorMessage ="Formato del nome errato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "L'attributo 'Cognome' è richiesto")]
        [RegularExpression("[A-Z][a-z]{1,14}", ErrorMessage = "Formato del cognome errato")]
        public string Cognome { get; set; }

        //navigatio property per associazione N:M
        public ICollection<Ordine> Ordini { get; }

    }
}
