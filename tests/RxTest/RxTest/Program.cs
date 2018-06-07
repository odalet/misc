using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace RxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(0.5));
            var subject = new Subject<long>();
            var subSource = source.Subscribe(subject);
            var subSubject1 = subject.Subscribe(
                                     x => Console.WriteLine("Value published to observer #1: {0}", x),
                                     () => Console.WriteLine("#1: Sequence Completed."));
            var subSubject2 = subject.Subscribe(
                                     x => Console.WriteLine("Value published to observer #2: {0}", x),
                                     () => Console.WriteLine("#2: Sequence Completed."));

            Thread.Sleep(2000);
            var subSubject3 = subject.Subscribe(
                                    x => Console.WriteLine("Value published to observer #3: {0}", x),
                                    () => Console.WriteLine("#3: Sequence Completed."));

            Thread.Sleep(2000);
            subSubject1.Dispose();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            subject.OnCompleted();
            subSubject2.Dispose();
            subSubject3.Dispose();
        }
    }
}
