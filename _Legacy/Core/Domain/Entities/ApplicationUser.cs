namespace Denounces.Domain.Entities
{
   
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [MaxLength(13)]
        public string Rnc { get; set; }

        [MaxLength(13)]
        public string Phone { get; set; }

        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string FullName => $"{Name} {Lastname}";

    }
}
