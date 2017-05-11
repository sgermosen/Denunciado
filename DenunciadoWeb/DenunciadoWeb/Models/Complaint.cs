using System;
using System.ComponentModel.DataAnnotations;

namespace DenunciadoBackEnd.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }
       
        [Required(ErrorMessage = "you must choose a {0}")]
        public int UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(250, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 10)]       
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(450, ErrorMessage = "The field {0} can't have more than {1} and less than {2} characters", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
     
        public string Image { get; set; }
        
        [Display(Name = "Is active")]
        public bool IsActive { get; set; }
               
        public decimal Lat { get; set; }
      
        public decimal Lon { get; set; }

       // [Required(ErrorMessage = "You must select a {0}")]
        public int ComplaintTypeId { get; set; }

        public virtual ComplaintType ComplaintType { get; set; }

    }
}