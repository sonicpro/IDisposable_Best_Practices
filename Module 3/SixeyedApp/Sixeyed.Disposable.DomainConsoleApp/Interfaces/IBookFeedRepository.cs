using Sixeyed.Disposable.DomainConsoleApp.Domain;

namespace Sixeyed.Disposable.DomainConsoleApp.Interfaces
{
    public interface IBookFeedRepository
    {
        BookFeed Get(int id);
        BookFeed Get(string path);
        void Add(BookFeed bookFeed);
        void Save();
    }
}