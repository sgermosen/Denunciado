using Denounces.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class Province : AuditEntity
    {
        [Required]
        [MaxLength(6)]
        public string Initials { get; set; }

    }
}
