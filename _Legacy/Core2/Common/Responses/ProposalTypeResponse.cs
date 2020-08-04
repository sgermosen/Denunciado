using System.Collections.Generic;

namespace Denounces.Web.Models
{
    public class ProposalTypeResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public ICollection<ProposalResponse> Proposals { get; set; }
    }
}
