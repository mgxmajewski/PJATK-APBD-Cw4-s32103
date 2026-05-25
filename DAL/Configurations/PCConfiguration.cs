using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL.Configurations;

public class PCConfiguration : IEntityTypeConfiguration<PC>
{
    public void Configure(EntityTypeBuilder<PC> builder)
    {
        builder.ToTable("PCs");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Weight).HasColumnType("float(5)");
        builder.Property(p => p.Warranty).IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(p => p.Stock).IsRequired();

        builder.HasData(
            new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
            new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new PC { Id = 3, Name = "Workstation Ultra", Weight = 18.0, Warranty = 48, CreatedAt = new DateTime(2026, 3, 1, 8, 0, 0), Stock = 3 }
        );
    }
}
