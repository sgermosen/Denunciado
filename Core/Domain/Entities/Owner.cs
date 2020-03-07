namespace Denounces.Domain.Entities.Cor
{
    using Helpers;
    using System;

    public class Owner : BaseEntity, IBaseEntity
    {

        public string Code { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Imagen { get; set; }

        public string Tel { get; set; }

        public string PrefixExp { get; set; }

        public string PrefixFact { get; set; }

        public string PrefixOrder { get; set; }

        public DateTime? StartDate { get; set; }

        public string PrefixNcf { get; set; }

        public int? SeqNcf { get; set; }

        public string PrefixFinalFact { get; set; }

        public string NcfEnds { get; set; }

        public int? SeqFact { get; set; }

        public string Header1 { get; set; }

        public string Header2 { get; set; }

        public string Header3 { get; set; }

        public string Footer1 { get; set; }

        public string Footer2 { get; set; }

        public string Footer3 { get; set; }

        public OwnerType OwnerType { get; set; }

    }
}
