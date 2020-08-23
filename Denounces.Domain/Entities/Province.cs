using Denounces.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class Province : BaseEntity
    {
        public long CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [MaxLength(6)]
        public string Initials { get; set; }

    }
}
