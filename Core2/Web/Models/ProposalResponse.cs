using System;
using System.Collections.Generic;

namespace Denounces.Web.Models
{
    public class ProposalResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ApplicationUserResponse CreatedUser { get; set; }

        public string Description { get; set; } //Resumen

        public string Details { get; set; } //Details/ Legal Mark

        public long Longitude { get; set; }

        public long Latitude { get; set; }

        public int PriorityNumber { get; set; }

        public DateTime? EndDate { get; set; }

        public int ProposalTypeId { get; set; }
        public ProposalTypeResponse ProposalType { get; set; }

        public int StatusId { get; set; }
        public StatusResponse Status { get; set; }

        public ICollection<VoteResponse> Votes { get; set; }




    }
}
