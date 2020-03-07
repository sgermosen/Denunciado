using Denounces.Domain.Entities.Cor;
using System.Collections.Generic;

namespace Denounces.Web.Models
{
    public class HomeViewModel
    {
       // public Owner Owner { get; set; }
        public IEnumerable<Person> Persons { get; set; }
    }
}