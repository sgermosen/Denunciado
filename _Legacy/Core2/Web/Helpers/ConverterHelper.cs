using Denounces.Domain.Entities;
using Denounces.Infraestructure;
using Denounces.Web.Models;
using System;
using System.Collections.Generic;

namespace Denounces.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly ApplicationDbContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(ApplicationDbContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public Proposal ToProposalEntity(ProposalViewModel model, string path, bool isNew)
        {
            return new Proposal
            {
                //Id = isNew ? 0 : model.Id,
                //LogoPath = path,
                //Name = model.Name
            };
        }

        public Vote ToVoteEntity(VoteViewModel model, string path, bool isNew)
        {
            throw new NotImplementedException();
        }

        public List<VoteResponse> ToVoteResponse(List<Vote> VoteEntities)
        {
            throw new NotImplementedException();
        }

        public VoteViewModel ToVoteViewModel(Vote vote)
        {
            throw new NotImplementedException();
        }

        ProposalViewModel ToProposalViewModel(Proposal entity)
        {
            return new ProposalViewModel
            {
                //Id = teamEntity.Id,
                //LogoPath = teamEntity.LogoPath,
                //Name = teamEntity.Name
            };
        }

        ProposalViewModel IConverterHelper.ToProposalViewModel(Proposal entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUserResponse ToUserResponse(ApplicationUser user)
        {
            if (user == null)
            {
                return null;
            }

            return new ApplicationUserResponse
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                Lastname = user.Lastname,
                //Team = ToTeamResponse(user?.Team),
                //UserType = user.UserType
            };
        }

       
    }
}
