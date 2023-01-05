using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class SecurityConfigurations : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Seguridad");

            builder.Property(e => e.Id)
                .HasColumnName("IdSeguridad")
                .IsRequired();
                

            builder.Property(e => e.User)
                 .HasColumnName("Usuario")
                 .HasMaxLength(50)
                 .IsRequired()
                 .IsUnicode(false);

            builder.Property(e => e.UserName)
                .HasColumnName("NombreUsuario")
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.PassWord)
                .HasColumnName("Contrasena")
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.Role)
                .HasColumnName("Rol")
                .HasMaxLength(15)
                .IsRequired()
                .HasConversion(
                x => x.ToString(),
                x => (RoleType)Enum.Parse(typeof(RoleType), x));
        }
    }
}
