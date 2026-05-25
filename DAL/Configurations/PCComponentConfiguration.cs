using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL.Configurations;

public class PCComponentConfiguration : IEntityTypeConfiguration<PCComponent>
{
    public void Configure(EntityTypeBuilder<PCComponent> builder)
    {
        builder.ToTable("PCComponents");
        builder.HasKey(pc => new { pc.PCId, pc.ComponentCode });
        builder.Property(pc => pc.ComponentCode).HasColumnType("char(10)");
        builder.Property(pc => pc.Amount).IsRequired();

        builder.HasOne(pc => pc.PC)
            .WithMany(p => p.PCComponents)
            .HasForeignKey(pc => pc.PCId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.Component)
            .WithMany(c => c.PCComponents)
            .HasForeignKey(pc => pc.ComponentCode)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 }
        );
    }
}
