namespace LogTailer.Data.Configurations
{
    using System.Drawing;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LineHighlightConfiguration : IEntityTypeConfiguration<LineHighlight>
    {
        public void Configure(EntityTypeBuilder<LineHighlight> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.Property(x => x.ForegroundColor)
                       .HasConversion(x => x.ToArgb(),
                           x => Color.FromArgb(x));

            _ = builder.Property(x => x.BackgroundColor)
                       .HasConversion(x => x.ToArgb(),
                           x => Color.FromArgb(x));
        }
    }
}
