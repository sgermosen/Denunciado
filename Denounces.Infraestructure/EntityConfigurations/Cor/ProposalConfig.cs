namespace Denounces.Infraestructure.EntityConfigurations.Cor
{
    using Denounces.Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProposalConfig
    {
        public ProposalConfig(EntityTypeBuilder<Proposal> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
             
            entityBuilder.Property(x => x.Address).HasMaxLength(300);
            entityBuilder.Property(x => x.Details).HasMaxLength(5000);

        }
    }
}
