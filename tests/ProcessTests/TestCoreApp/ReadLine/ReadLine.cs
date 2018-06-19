using Internal.ReadLine;
using Internal.ReadLine.Abstractions;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class ReadLine
    {
        private static List<string> _history;

        static ReadLine()
        {
            _history = new List<string>();
        }

        public static void AddHistory(params string[] text) => _history.AddRange(text);
        public static List<string> GetHistory() => _history;
        public static void ClearHistory() => _history = new List<string>();
        public static bool HistoryEnabled { get; set; }
        public static IAutoCompleteHandler AutoCompletionHandler { private get; set; }

        public static string Read(CancellationToken? cancellationToken = null, string prompt = "", string @default = "")
        {
            Console.Write(prompt);
            var keyHandler = new KeyHandler(new Console2(), _history, AutoCompletionHandler);
            var text = cancellationToken.HasValue ?
                GetText(cancellationToken.Value, keyHandler) : GetText(keyHandler);

            if (String.IsNullOrWhiteSpace(text) && !String.IsNullOrWhiteSpace(@default))
            {
                text = @default;
            }
            else
            {
                if (HistoryEnabled)
                    _history.Add(text);
            }

            return text;
        }

        public static string ReadPassword(CancellationToken? cancellationToken = null, string prompt = "")
        {
            Console.Write(prompt);
            KeyHandler keyHandler = new KeyHandler(new Console2() { PasswordMode = true }, null, null);
            return cancellationToken.HasValue ?
                GetText(cancellationToken.Value, keyHandler) : GetText(keyHandler);
        }

        private static string GetText(CancellationToken cancellationToken, KeyHandler keyHandler)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    while (!Console.KeyAvailable)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        Thread.Sleep(50);
                    }

                    return GetText(keyHandler);

                    ////ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ////while (keyInfo.Key != ConsoleKey.Enter)
                    ////{
                    ////    keyHandler.Handle(keyInfo);
                    ////    keyInfo = Console.ReadKey(true);
                    ////}

                    ////Console.WriteLine();
                }
                catch (OperationCanceledException)
                {
                    // Do nothing, simply return
                    return string.Empty;
                }
            });
            
            task.Wait();

            return task.Result;
        }

        private static string GetText(KeyHandler keyHandler)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyHandler.Handle(keyInfo);
                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return keyHandler.Text;
        }
    }
}
