namespace Sixeyed.Disposable.DomainConsoleApp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookLine")]
    public partial class BookLine
    {
        public int BookFeedId { get; set; }

        public int LineNumber { get; set; }

        public int WordCount { get; set; }

        [StringLength(1000)]
        public string Excerpt { get; set; }

        public int Id { get; set; }

        public virtual BookFeed BookFeed { get; set; }
    }
}
