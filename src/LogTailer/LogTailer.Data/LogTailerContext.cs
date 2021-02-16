namespace LogTailer.Data
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class LogTailerContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<OpenFile> OpenFiles { get; set; }
        public DbSet<LineHighlight> LineHighlights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=logtailer.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogTailerContext).Assembly);
    }
}
