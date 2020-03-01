using Sixeyed.Disposable.DomainConsoleApp.Domain;
using System;

namespace Sixeyed.Disposable.DomainConsoleApp.Interfaces
{
    // Bad OO design ahead!
    // IBookFeedRepository shouldn't mandate that implementers of it should use IDisposable fields in the implementation.
    // But declaring IBookFeedRepository as well as the rest interfaces as IDisposable makes IDisposable instances
    // easily discoverable; the practical benefit is worth the design smell.
    public interface IBookFeedRepository : IDisposable
    {
        BookFeed Get(int id);
        BookFeed Get(string path);
        void Add(BookFeed bookFeed);
        void Save();
    }
}