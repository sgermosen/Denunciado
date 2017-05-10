using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DenunciadoWeb.Models
{
    public class ComplaintsType
    {

        [Key]
        public int ComplaintTypeId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(15, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 1)]
        [Index("ComplaintType_Code_Index", IsUnique = true)]
        public string Code { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(250, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 5)]
        [Index("ComplaintType_Description_Index", IsUnique = true)]
        public string Description { get; set; }

    }
}