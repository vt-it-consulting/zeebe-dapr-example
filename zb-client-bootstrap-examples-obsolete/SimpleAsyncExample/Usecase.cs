using System;
using System.Threading.Tasks;

namespace SimpleAsyncExample
{
    static class Usecase
    {
        public static Task ExecuteAsync()
        {
            Console.WriteLine("ExecuteAsync()...");

            return Task.CompletedTask;
        }
    }
}