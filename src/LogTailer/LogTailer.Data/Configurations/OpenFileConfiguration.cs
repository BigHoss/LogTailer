namespace LogTailer.Data.Configurations
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OpenFileConfiguration : IEntityTypeConfiguration<OpenFile>
    {
        public void Configure(EntityTypeBuilder<OpenFile> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.HasOne(x => x.Session)
                       .WithMany(x => x.OpenFiles);
        }
    }
}
