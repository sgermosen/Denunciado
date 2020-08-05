namespace Denounces.Infraestructure.EntityConfigurations.Cor
{
    using Domain.Entities.Cor;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OwnerConfig
    {
        public OwnerConfig(EntityTypeBuilder<Owner> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Code).IsRequired().HasMaxLength(15);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            //[MaxLength(50, ErrorMessage = "La longitud maxima del campo es {1} caracteres")]
            //[DataType(DataType.EmailAddress)]
            //[Index("Author_Email_Index", IsUnique = true)]
            //[Display(Name = "Correo")]
            entityBuilder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            //  [DataType(DataType.ImageUrl)]
            //[MaxLength(50, ErrorMessage = "La longitud maxima del campo es {1} caracteres")]
            //[DataType(DataType.PhoneNumber)]
            //[Index("Author_Tel_Index", IsUnique = true)]

            entityBuilder.Property(x => x.Tel).HasMaxLength(15);
            entityBuilder.Property(x => x.PrefixExp).HasMaxLength(10);
            entityBuilder.Property(x => x.PrefixFact).HasMaxLength(15);
            entityBuilder.Property(x => x.PrefixOrder).HasMaxLength(10);
            entityBuilder.Property(x => x.PrefixNcf).HasMaxLength(16);
            entityBuilder.Property(x => x.PrefixFinalFact).HasMaxLength(18);
            entityBuilder.Property(x => x.NcfEnds).HasMaxLength(25);
            entityBuilder.Property(x => x.Header1).HasMaxLength(100);
            entityBuilder.Property(x => x.Header2).HasMaxLength(100);
            entityBuilder.Property(x => x.Header3).HasMaxLength(100);
            entityBuilder.Property(x => x.Footer1).HasMaxLength(100);
            entityBuilder.Property(x => x.Footer2).HasMaxLength(100);
            entityBuilder.Property(x => x.Footer3).HasMaxLength(100);

        }
    }
}
