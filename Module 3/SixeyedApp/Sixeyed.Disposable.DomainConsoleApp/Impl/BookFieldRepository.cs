using System.Linq;
using Sixeyed.Disposable.DomainConsoleApp.Domain;
using Sixeyed.Disposable.DomainConsoleApp.Interfaces;

namespace Sixeyed.Disposable.DomainConsoleApp.Impl
{
    class BookFeedRepository : IBookFeedRepository
    {
        private BookFeedContext _context;

        public BookFeedRepository()
        {
            _context = new BookFeedContext();
        }

        public BookFeed Get(int id)
        {
            return _context.BookFeed.Find(id);
        }

        public BookFeed Get(string path)
        {
            return _context.BookFeed.FirstOrDefault(x => x.Path == path);
        }

        public void Add(BookFeed bookFeed)
        {
            _context.BookFeed.Add(bookFeed);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
