using System;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetGenerate
{
    public class Program
    {
        private readonly IConsole _console;

        public Program(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException();

            _console = console;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
