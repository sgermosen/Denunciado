using System.ComponentModel.DataAnnotations.Schema;


namespace DenunciadoBackEnd.Models
{
    [NotMapped]
    public class ComplaintRequest:Complaint
    {
        public byte[] ImageArray { get; set; }
    }
}