namespace LogTailer.Data.Configurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.Property(x => x.RibbonCollapsed)
                       .IsUnicode(false);
        }
    }
}
