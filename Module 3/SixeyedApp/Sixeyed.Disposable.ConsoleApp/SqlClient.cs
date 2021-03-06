﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sixeyed.Disposable.ConsoleApp
{
    public class SqlClient
    {
        public void CreateFeedResults(string path, int lineCount, int wordCount, long processingMilliseconds)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Books"].ConnectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format(
                        "INSERT INTO BookFeed (Path, LineCount, WordCount, ProcessingMilliseconds) VALUES ('{0}', {1}, {2}, {3});",
                        path, lineCount, wordCount, processingMilliseconds);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFeedResults(string path, int lineCount, int wordCount, long processingMilliseconds)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Books"].ConnectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format(
                        "UPDATE BookFeed SET LineCount = {1}, WordCount = {2}, ProcessingMilliseconds = {3} WHERE Path = '{0}';",
                        path, lineCount, wordCount, processingMilliseconds);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveLineResults(string path, int lineNumber, int wordCount, string excerpt)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Books"].ConnectionString))
            {
                sqlConnection.Open();

                var bookFeedId = 0;

                using (var selectCommand = sqlConnection.CreateCommand())
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.CommandText = string.Format("SELECT Id FROM BookFeed WHERE Path='{0}'", path);
                    bookFeedId = (int)selectCommand.ExecuteScalar();
                }

                using (var insertCommand = sqlConnection.CreateCommand())
                {
                    insertCommand.CommandType = CommandType.Text;
                    insertCommand.CommandText = string.Format(
                        "INSERT INTO BookLine (BookFeedId, LineNumber, WordCount, Excerpt) VALUES ('{0}', {1}, {2}, '{3}');",
                        bookFeedId, lineNumber, wordCount, excerpt.Replace("'", "''"));
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
