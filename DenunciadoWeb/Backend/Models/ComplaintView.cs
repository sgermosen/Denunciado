using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain;

namespace Backend.Models
{
    [NotMapped]
    public class ComplaintView : Complaint
    {
        [Display(Name = "Picture")]
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}