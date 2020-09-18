using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework.Models
{
    public class Cliente
    {
        private string nome;
        public Cliente()
        {
            // Inizializzo una collection vuota.
            Ordini = new List<Ordine>();
        }

        [Key][Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "L'attributo 'Nome' è richiesto")]
        [RegularExpression("[A-Z][a-z]{1,14}", ErrorMessage = "Formato del nome errato")]
        public string Nome { get => nome; set { nome = value; } }

        [Required(ErrorMessage = "L'attributo 'Cognome' è richiesto")]
        [RegularExpression("[A-Z][a-z]{1,14}", ErrorMessage = "Formato del cognome errato")]
        public string Cognome { get; set; }

        [NotMapped] //indica a EF di non andarlo a cercare nello schema della relazione Cliente
        public string NomeCognome { get => $"{Nome}_{Cognome}"; }
         //dato che NomeCognome non esiste come attributo del db, con NotMapped EF non va acercarlo in tabella
        //public string NomeCognome{ get => nomecognome; set { nomecognome =  } }

        //navigatio property per associazione N:M
        public ICollection<Ordine> Ordini { get; }

    }
}
