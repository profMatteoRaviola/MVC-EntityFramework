using Eccezioni;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace MVC_EntityFramework.Models
{
    public class Ordine
    {
        public Ordine()
        {
            // Inizializzo una collection vuota.
            Dettaglio_Ordini = new List<Dettaglio_Ordine>();
        }

        [Key] [Required] public int Id { get; set; } //key primaria dell'ordine

        
        [Required] public int ClienteId { get; set; } //foreign key verso il cliente

        [Required(ErrorMessage = "L'attributo 'Data' è richiesto")]
        public DateTime Data { get; set; }

        private string mod_pagamento;
        [Required(ErrorMessage = "L'attributo 'Modalità di pagamento' è richiesto")]
        public string Mod_pagamento
        {
            get { return mod_pagamento; }
            set
            {
                mod_pagamento = value switch //switch con sintassi funzionale... tutto autogenerato
                {
                    "Contrassegno" => value,
                    "PayPal" => value,
                    _ => throw new PagamentoException(value) //default
                };

            }
        }
        public Cliente Cliente { get; set; }
        public ICollection<Dettaglio_Ordine> Dettaglio_Ordini { get; }
    }
}
