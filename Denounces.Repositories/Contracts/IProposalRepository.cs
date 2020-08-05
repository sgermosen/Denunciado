using System;
namespace Denounces.Repositories.Contracts
{
    using Domain.Entities;
    using System.Threading.Tasks;

    public interface IProposalRepository : IRepository<Proposal>
    {
        Task<Proposal> AddProposalAsync(Proposal entity);
    }
}
