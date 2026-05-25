using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL.Configurations;

public class ComponentTypeConfiguration : IEntityTypeConfiguration<ComponentType>
{
    public void Configure(EntityTypeBuilder<ComponentType> builder)
    {
        builder.ToTable("ComponentTypes");
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Id).ValueGeneratedOnAdd();
        builder.Property(ct => ct.Abbreviation).HasMaxLength(30).IsRequired();
        builder.Property(ct => ct.Name).HasMaxLength(150).IsRequired();

        builder.HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        );
    }
}
