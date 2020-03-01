using Sixeyed.Disposable.ConsoleApp.ServiceAgents.WordCount;
using System;

namespace Sixeyed.Disposable.ConsoleApp
{
    public class ApiClient
    {
        public int GetWordCount(string input)
        {
            var wordCount = 0;
            var client = new WordCountServiceClient();
            try
            {
                wordCount = client.GetWordCount(input);

                // System.ServiceModel.Client.Close() is called from its Dispose() and that throws an exception if the call was failed.
                // We cannot use a simple using statement with WCF Client instances.
                client.Close();  
            }
            catch
            {
                client.Abort();
            }
            finally
            {
                ((IDisposable)client).Dispose();
            }
            return wordCount;
        }
    }
}
