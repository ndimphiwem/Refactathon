using System;
using System.Threading;

namespace Refactoring.Conway
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            void OnCancelKeyPress(object sender, ConsoleCancelEventArgs args)
            {
                args.Cancel = true;
                cancellationTokenSource.Cancel();
            }

            Console.CancelKeyPress += OnCancelKeyPress;

            Startup.Execute(cancellationTokenSource);

            Console.CancelKeyPress -= OnCancelKeyPress;
        }

        
    }
}
