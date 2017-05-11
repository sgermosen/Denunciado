using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DenunciadoBackEnd.Models
{
    public class ComplaintView
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}