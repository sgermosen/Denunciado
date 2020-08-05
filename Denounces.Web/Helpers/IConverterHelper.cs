using Denounces.Domain.Entities;
using Denounces.Web.Models;
using System.Collections.Generic;

namespace Denounces.Web.Helpers
{
    public interface IConverterHelper
    {
 
        ApplicationUserResponse ToUserResponse(ApplicationUser user);

        Proposal ToProposalEntity(ProposalViewModel model, string path, bool isNew);

        ProposalViewModel ToProposalViewModel(Proposal entity);

        Vote ToVoteEntity(VoteViewModel model, string path, bool isNew);

        VoteViewModel ToVoteViewModel(Vote vote);

        List<VoteResponse> ToVoteResponse(List<Vote> VoteEntities);
    }
}
