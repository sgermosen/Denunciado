using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Denounces.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboProposalTypes();

        IEnumerable<SelectListItem> GetComboProposalTypes(long id);
    }
}
