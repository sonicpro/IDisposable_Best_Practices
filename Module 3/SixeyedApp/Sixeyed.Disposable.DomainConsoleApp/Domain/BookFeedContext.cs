namespace Sixeyed.Disposable.DomainConsoleApp.Domain
{
    using System.Data.Entity;

    public partial class BookFeedContext : DbContext
    {
        public BookFeedContext()
            : base("name=Books")
        {
        }

        public virtual DbSet<BookFeed> BookFeed { get; set; }
        public virtual DbSet<BookLine> BookLine { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookFeed>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<BookFeed>()
                .HasMany(e => e.BookLines)
                .WithRequired(e => e.BookFeed)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BookLine>()
                .Property(e => e.Excerpt)
                .IsUnicode(false);
        }
    }
}
