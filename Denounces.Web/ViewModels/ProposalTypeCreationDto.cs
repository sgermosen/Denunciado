using System;
using Denounces.Domain.Entities;
using Denounces.Domain.Helpers;
using Denounces.Infraestructure.Validations;
using Microsoft.AspNetCore.Http;

namespace Denounces.Web.ViewModels
{
    public class ProposalTypeCreationDto: ProposalType
    {
        [FileSizeValitadion(maxSizeInMegabytes: 4)]
        [FileTypeValidation(groupTypeFile: FileType.Image)]
        public IFormFile Picture { get; set; }
    }
}
