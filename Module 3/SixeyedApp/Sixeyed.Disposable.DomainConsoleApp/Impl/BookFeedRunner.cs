﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Sixeyed.Disposable.DomainConsoleApp.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Sixeyed.Disposable.DomainConsoleApp.Domain;

namespace Sixeyed.Disposable.DomainConsoleApp.Impl
{
    class BookFeedRunner : IBookFeedRunner
    {
        private IBookFeedRepository _repository;
        private readonly IStreamUser _streamUser;
        private IFileArchiver _fileArchiver;

        public BookFeedRunner(IBookFeedRepository repository, IStreamUser streamUser, IFileArchiver fileArchiver)
        {
            _repository = repository;
            _streamUser = streamUser;
            _fileArchiver = fileArchiver;
        }

        public void Start()
        {
            _fileArchiver.Start(@"c:\books", "*.txt", ProcessFile);
            Console.WriteLine("Listening on c:\\books");
        }

        private void ProcessFile(string path)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Processing file: " + path);
            var archivePath = string.Format(@"c:\books\archive\{0}.txt", Guid.NewGuid());
            Console.WriteLine("Archiving to: " + archivePath);
            _streamUser.CopyFile(path, archivePath);

            Console.WriteLine("Initialising db results");
            var bookFeed = new BookFeed
                {
                    Path = path,
                    LineCount = 0,
                    WordCount = 0,
                    ProcessingMilliseconds = 0
                };
            _repository.Add(bookFeed);
            _repository.Save();

            var cancellationTokenSource = new CancellationTokenSource();

            var lines = File.ReadAllLines(path);
            var wordCounts = new ConcurrentBag<int>();
            var apiTasks = new List<Task>();
            for (var i = 0; i < lines.Length; i++)
            {
                var lineNumber = i;
                var line = lines[i];
                apiTasks.Add(Task.Factory.StartNew(() => GetWordCount(path, lineNumber, line, wordCounts, cancellationTokenSource)));
            }
            try
            {
                Task.WaitAll(apiTasks.ToArray(), cancellationTokenSource.Token);
                var wordCount = wordCounts.Sum();

                Console.WriteLine("Saving results to db");
                bookFeed = _repository.Get(path);
                bookFeed.LineCount = lines.Length;
                bookFeed.WordCount = wordCount;
                bookFeed.ProcessingMilliseconds = stopwatch.ElapsedMilliseconds;
                _repository.Save();
                Console.WriteLine("Complete, took: {0}ms", stopwatch.ElapsedMilliseconds);
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Feed errored: " + ex.Message);
            }
        }

        private static void GetWordCount(string path, int lineNumber, string line, ConcurrentBag<int> wordCounts, CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();

            try
            {
                var excerpt = line.Length > 100 ? line.Substring(0, 100) : line;
                Console.WriteLine("Processing line: {0}, '{1}...'", lineNumber, excerpt);
                var apiClient = Container.Resolve<IApiClient>();
                var wordCount = apiClient.GetWordCount(line);
                var repository = Container.Resolve<IBookFeedRepository>();
                var bookFeed = repository.Get(path);
                bookFeed.BookLines.Add(new BookLine
                    {
                        LineNumber = lineNumber,
                        WordCount = wordCount,
                        Excerpt = excerpt
                    });
                repository.Save();
                wordCounts.Add(apiClient.GetWordCount(line));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                cancellationTokenSource.Cancel();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_fileArchiver !=  null)
                {
                    _fileArchiver.Dispose();
                    _fileArchiver = null;
                }
                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }
        }
    }
}
