using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Eccezioni;

namespace MVC_EntityFramework.Models
{
    public class Giardino
    {
        [Key][Required]
        public int PiantaID { get; set; } //chiave esterna della pianta che diventa primaria per una pianta da giardino

        private string stagione;
        [Required(ErrorMessage = "Attributo 'Stagione' richiesto")]
       
        public string Stagione {
            get => stagione;
            set
            {
                stagione = value switch
                {
                    "Estate" => value,
                    "Primavera" => value,
                    "Autunno" => value,
                    "Inverno" => value,
                    _ => throw new StagioneException(value),
                };
            } 
        }

        public Pianta Pianta { get; set; }
    }
}
