using System.Collections.ObjectModel;

namespace Denounces.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GenericSelectList
    {
        public IEnumerable<SelectListItem> CreateSelectList<T>(IList<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            var r = new List<SelectListItem> {new SelectListItem {Value = "0", Text = "_Seleccione Una Opción"}};

            r.AddRange(entities
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                }).OrderBy(p => p.Text));
            return r;
        }
        public IEnumerable<SelectListItem> CreateSelectListWithNoChoose<T>(IList<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            var r = new List<SelectListItem> ();

            r.AddRange(entities
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                }).OrderBy(p => p.Text));
            return r;
        }
    }
}
