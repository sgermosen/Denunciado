using System;

namespace Denounces.Web.Models
{
    public class VoteResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ApplicationUserResponse CreatedUser { get; set; }

        public int ProposalId { get; set; }

        public bool IsFavorVote { get; set; }

        public bool IsNegativeVote { get; set; }

        public bool IsNullVote { get; set; }

        public ProposalResponse Proposal { get; set; }
    }
}
