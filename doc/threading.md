# Thread based devices management

* [https://codereview.stackexchange.com/questions/9812/polling-loop-always-a-bad-decision](https://codereview.stackexchange.com/questions/9812/polling-loop-always-a-bad-decision)
* [https://codereview.stackexchange.com/questions/157909/thread-safe-task-queue-implementation](https://codereview.stackexchange.com/questions/157909/thread-safe-task-queue-implementation)

//google: c# threaded queue loop site:codereview.stackexchange.com

* [https://codereview.stackexchange.com/questions/151562/loop-for-periodic-processing-in-a-background-thread](https://codereview.stackexchange.com/questions/151562/loop-for-periodic-processing-in-a-background-thread)
* [https://codereview.stackexchange.com/questions/82374/is-it-ok-to-use-thread-sleep-and-thread-interrupt-for-pausing-and-resuming-threa](https://codereview.stackexchange.com/questions/82374/is-it-ok-to-use-thread-sleep-and-thread-interrupt-for-pausing-and-resuming-threa)

--> See Observable... at the end

* [https://stackoverflow.com/questions/46591555/how-to-do-proper-producer-consumer-pattern-with-rx](https://stackoverflow.com/questions/46591555/how-to-do-proper-producer-consumer-pattern-with-rx)

* [https://codereview.stackexchange.com/questions/152764/single-thread-worker-in-multi-thread-webservice](https://codereview.stackexchange.com/questions/152764/single-thread-worker-in-multi-thread-webservice)

* [https://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher(v=vs.110).aspx](https://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher(v=vs.110).aspx)

* [https://msdn.microsoft.com/en-us/library/hh242970(v=vs.103).aspx](https://msdn.microsoft.com/en-us/library/hh242970(v=vs.103).aspx)



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

