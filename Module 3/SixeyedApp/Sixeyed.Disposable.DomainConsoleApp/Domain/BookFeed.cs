namespace Sixeyed.Disposable.DomainConsoleApp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookFeed")]
    public partial class BookFeed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookFeed()
        {
            BookLines = new HashSet<BookLine>();
        }

        public int Id { get; set; }

        [StringLength(256)]
        public string Path { get; set; }

        public int? LineCount { get; set; }

        public int? WordCount { get; set; }

        public long? ProcessingMilliseconds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookLine> BookLines { get; set; }
    }
}
