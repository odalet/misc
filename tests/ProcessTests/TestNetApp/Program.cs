using System;
using System.Diagnostics;
using System.Threading;

namespace TestNetApp
{
    internal class MainProcess
    {
        private readonly CancellationTokenSource cts;
        private readonly Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainProcess"/> class.
        /// </summary>
        public MainProcess(bool interactive)
        {
            IsInteractive = interactive;
            cts = new CancellationTokenSource();
            thread = new Thread(Worker);
        }

        public event EventHandler Terminated;

        public bool IsInteractive { get; }

        public void Start() => thread.Start(cts.Token);

        public void Stop()
        {
            cts.Cancel();
            // here should be the thread cleanup code...
            cts.Dispose();
            thread.Interrupt(); // Makes sure we exit the thread...
        }

        private void Worker(object cancellationToken)
        {
            try
            {
                var token = (CancellationToken)cancellationToken;
                while (!token.IsCancellationRequested)
                {
                    using (var me = Process.GetCurrentProcess())
                        Console.WriteLine($"Worker: {DateTime.Now} - Hello, I am {me.ProcessName} ({me.Id}) invoked by {Environment.CommandLine}");

                    if (IsInteractive)
                    {
                        Console.Write("q to exit: ");
                        var read = Console.ReadLine();
                        Console.WriteLine(" line read...");
                        if (token.IsCancellationRequested || read?.ToLowerInvariant() == "q")
                            break;
                    }
                    else Thread.Sleep(1000);
                }

                Console.WriteLine("## Worker: thread ended by user");
                Terminated?.Invoke(this, EventArgs.Empty);
            }
            catch (ThreadInterruptedException)
            {
                // This is used to force us out of the ReadLine call...
                Console.WriteLine("## Worker: thread interrupted by caller");
            }
        }
    }

    // http://mikehadlow.blogspot.com/2013/04/stop-your-console-app-nice-way.html
    internal static class Program
    {
        private static int waitBeforeClosing;
        private static AutoResetEvent terminationRequested = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            if (args.Length > 0)
                waitBeforeClosing = int.Parse(args[0]);

            Console.WriteLine("Main: Application has started. Ctrl-C to end");

            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Console.WriteLine("----> Process Exit Detected");
                CleanupBeforeExit("ProcessExit");
            };

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                // cancel the cancellation to allow the program to shutdown cleanly
                eventArgs.Cancel = true;
                terminationRequested.Set();
            };

            var mainProcess = new MainProcess(false);
            mainProcess.Terminated += (s, e) => terminationRequested.Set();
            mainProcess.Start();

            // main blocks here waiting for ctrl-C            
            terminationRequested.WaitOne();
            Console.WriteLine("Main: Now shutting down");
            mainProcess.Stop();
        }

        private static void CleanupBeforeExit(string caller)
        {
            if (waitBeforeClosing > 0)
            {
                Console.WriteLine($"CleanupBeforeExit: Waiting for {waitBeforeClosing} ms before closing...");
                Thread.Sleep(waitBeforeClosing);
            }

            Console.WriteLine($"CleanupBeforeExit: {caller} -> Now exiting...");
        }
    }

    ////////internal class MainProcess
    ////////{
    ////////    private readonly Thread thread;
    ////////    private bool cancel;

    ////////    /// <summary>
    ////////    /// Initializes a new instance of the <see cref="MainProcess"/> class.
    ////////    /// </summary>
    ////////    public MainProcess(bool interactive)
    ////////    {
    ////////        IsInteractive = interactive;
    ////////        thread = new Thread(Worker);
    ////////    }

    ////////    public event EventHandler Terminated;

    ////////    public bool IsInteractive { get; }

    ////////    public void Start() => thread.Start();

    ////////    public void Stop()
    ////////    {
    ////////        cancel = true;
    ////////        // here should be the thread cleanup code...
    ////////        thread.Abort();
    ////////    }

    ////////    private void Worker()
    ////////    {
    ////////        while (!cancel)
    ////////        {
    ////////            using (var me = Process.GetCurrentProcess())
    ////////                Console.WriteLine($"Worker: {DateTime.Now} - Hello, I am {me.ProcessName} ({me.Id}) invoked by {Environment.CommandLine}");

    ////////            if (IsInteractive)
    ////////            {
    ////////                Console.Write("q to exit: ");
    ////////                var read = Console.ReadLine();
    ////////                Console.WriteLine(" line read...");
    ////////                if (read?.ToLowerInvariant() == "q")
    ////////                    break;
    ////////            }
    ////////            else Thread.Sleep(1000);
    ////////        }

    ////////        Console.WriteLine("Worker: thread ended by user");
    ////////        Terminated?.Invoke(this, EventArgs.Empty);
    ////////    }
    ////////}

    ////////// http://mikehadlow.blogspot.com/2013/04/stop-your-console-app-nice-way.html
    ////////internal static class Program
    ////////{
    ////////    private static int waitBeforeClosing;
    ////////    private static AutoResetEvent terminationRequested = new AutoResetEvent(false);

    ////////    static void Main(string[] args)
    ////////    {
    ////////        if (args.Length > 0)
    ////////            waitBeforeClosing = int.Parse(args[0]);

    ////////        Console.WriteLine("Main: Application has started. Ctrl-C to end");

    ////////        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
    ////////        {
    ////////            Console.WriteLine("----> Process Exit Detected");
    ////////            CleanupBeforeExit("ProcessExit");
    ////////        };

    ////////        Console.CancelKeyPress += (sender, eventArgs) =>
    ////////        {
    ////////            // cancel the cancellation to allow the program to shutdown cleanly
    ////////            eventArgs.Cancel = true;
    ////////            terminationRequested.Set();
    ////////        };

    ////////        var mainProcess = new MainProcess(true);
    ////////        mainProcess.Terminated += (s, e) => terminationRequested.Set();
    ////////        mainProcess.Start();

    ////////        // main blocks here waiting for ctrl-C            
    ////////        terminationRequested.WaitOne();
    ////////        Console.WriteLine("Main: Now shutting down");
    ////////        mainProcess.Stop();
    ////////    }

    ////////    private static void CleanupBeforeExit(string caller)
    ////////    {
    ////////        if (waitBeforeClosing > 0)
    ////////        {
    ////////            Console.WriteLine($"CleanupBeforeExit: Waiting for {waitBeforeClosing} ms before closing...");
    ////////            Thread.Sleep(waitBeforeClosing);
    ////////        }

    ////////        Console.WriteLine($"CleanupBeforeExit: {caller} -> Now exiting...");
    ////////    }
    ////////}
}
