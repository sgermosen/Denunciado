namespace Denounces.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public long ProvinceId { get; set; }
        public Province Province { get; set; }

        [MaxLength(13)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(13)]
        public string Rnc { get; set; }
         
        public string FullName => $"{Name} {Lastname}";

    }
}
