using Denounces.Domain.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Denounces.Domain.Entities
{
    public class Complaint : AuditEntity, IBaseEntity
    {      

        [Required]
        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreationDate { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        [StringLength(450, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public bool IsActive { get; set; }

        public decimal Lat { get; set; }

        public decimal Lon { get; set; }

        // [Required]
        public int ComplaintTypeId { get; set; }

        public ComplaintsType ComplaintType { get; set; }
        public ApplicationUser User { get; set; }
      }
}
