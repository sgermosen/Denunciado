using Denounces.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class Ideology : AuditEntity
    {
      
        //[Required]
        //[StringLength(50, ErrorMessage = "La longitud maxima para el campo {0}  es: {1} y el minimo es: {2}", MinimumLength = 1)]
        //[Display(Name = "Ideologia Politica")]
        //public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        public ICollection<ApplicationUser> Fans { get; set; }


    }
}
