using Denounces.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denounces.Domain.Entities
{
    public class ComplaintsType : AuditEntity
    {
        [Required]
        [StringLength(15, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 1)]
       // [Index("ComplaintType_Code_Index", IsUnique = true)]
        public string Code { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 5)]
        //[Index("ComplaintType_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        public ICollection<Complaint> Complaints { get; set; }

    }
}
