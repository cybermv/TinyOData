namespace TestConsole
{
    using Microsoft.Owin.Hosting;
    using System;
    using System.Diagnostics;

    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "http://localhost:4321";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started and running at {0}...", url);
                Process.Start(url);
                Console.ReadKey();
            }
        }
    }
}