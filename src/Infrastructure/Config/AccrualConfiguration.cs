using Domain.AccrualAggregate;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class AccrualConfiguration : IEntityTypeConfiguration<Accrual>
{
    public void Configure(EntityTypeBuilder<Accrual> builder)
    {
        builder.ToTable("accruals");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.AccrualStatus)
            .IsRequired()
            .HasColumnName("accrual_status")
            .HasColumnType("varchar(255)")
            .HasConversion(
                x => x.ToString(),
                x => (AccrualStatus)Enum.Parse(typeof(AccrualStatus), x)
            );

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnName("amount");

        builder.Property(p => p.MemberNumber)
            .IsRequired()
            .HasColumnName("member_number");

        builder.Property(p => p.AccrualDate)
            .IsRequired()
            .HasColumnName("accrual_date");

        builder.Property(p => p.ExpirationDate)
            .IsRequired()
            .HasColumnName("expiration_date");

        builder.Property(p => p.Partner)
            .IsRequired()
            .HasColumnName("partner")
            .HasColumnType("varchar(255)")
            .HasConversion(
                x => x.ToString(),
                x => (Partner)Enum.Parse(typeof(Partner), x)
            );
        
        builder.Property((p => p.PhoneNumber))
            .HasColumnName("phone_number")
            .HasColumnType("varchar(20)")
            .HasConversion(
                x => x.GetFullNumber(),
                x => new PhoneNumber(string.Empty, string.Empty, x)
            );
    }
}