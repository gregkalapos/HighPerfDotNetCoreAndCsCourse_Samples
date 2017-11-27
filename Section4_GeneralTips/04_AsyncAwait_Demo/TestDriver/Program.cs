using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestDriver
{
	/// <summary>
	/// This small app is the test driver that we use in video 4.4
	/// It creates 50000 web requests to an URL. The idea behind this is that we simulate load with this app
	/// and compare the performance of 2 asp.net core application: 1 doing an async outgoing HTTP call, and 1 doing a synchronous HTTP call
	/// </summary>
	class Program
    {
        static async Task Main(string[] args) //async main: C# 7.1 feature
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 50000; i++)
            {
                HttpClient httpClient = new HttpClient();
                //async: 54597
                //sync: 54634
                var task = httpClient.GetAsync("http://localhost:54634/api/values").ContinueWith((t) =>
                {
                    Console.WriteLine("Request finished");
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Console.WriteLine($"1000 requests done: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
