namespace Denounces.Web.ViewModels
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    public class ProposalCreateViewModel : Proposal
    {
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
