# Modular Architecture & Micro-Services

## TODO

* Simplifier la paire Module/Definition --> Tout dans Definition
* Simplifier le fx de validation ?
	* Moins fonctionnel, plus procédural
* Revoir les interfaces d'exposition des résultats de validation
	* Done: moins d'interfaces ; on renvoie directement des `ValidationRule<string>`

## Threading

* Limiting concurrent threads: [http://markheath.net/post/constraining-concurrent-threads-csharp](http://markheath.net/post/constraining-concurrent-threads-csharp)

## Micro-services

* Polly (circuit breaker and the likes...): [https://github.com/App-vNext/Polly](https://github.com/App-vNext/Polly)
	* [https://github.com/App-vNext/Polly/wiki/Circuit-Breaker](https://github.com/App-vNext/Polly/wiki/Circuit-Breaker)
	* [https://github.com/App-vNext/Polly/wiki/Bulkhead](https://github.com/App-vNext/Polly/wiki/Bulkhead)


## Process-based Architecture

* Brighter: [https://brightercommand.github.io/Brighter/](https://brightercommand.github.io/Brighter/)

## Process Management

* [https://softwarerecs.stackexchange.com/questions/18860/c-library-for-windows-process-management](https://softwarerecs.stackexchange.com/questions/18860/c-library-for-windows-process-management)
	* [https://archive.codeplex.com/?p=psinterop](https://archive.codeplex.com/?p=psinterop)
	* [http://www.crawler-lib.net/child-processes](http://www.crawler-lib.net/child-processes)
	* [https://archive.codeplex.com/?p=wolfpack](https://archive.codeplex.com/?p=wolfpack)
	* [https://stackoverflow.com/questions/3740256/process-management-in-net](https://stackoverflow.com/questions/3740256/process-management-in-net)
		* Job Objects: [https://msdn.microsoft.com/en-us/library/ms684161.aspx](https://msdn.microsoft.com/en-us/library/ms684161.aspx)
	* [https://stackoverflow.com/questions/15528015/what-is-the-difference-between-a-saga-a-process-manager-and-a-document-based-ap](https://stackoverflow.com/questions/15528015/what-is-the-difference-between-a-saga-a-process-manager-and-a-document-based-ap)
* [https://stackoverflow.com/questions/8153358/management-running-process](https://stackoverflow.com/questions/8153358/management-running-process)
* [https://social.msdn.microsoft.com/Forums/vstudio/en-US/4f8ac9a7-1c80-4294-8d8e-8e233bf8c521/c-console-app-to-monitor-a-process-and-its-cpu?forum=csharpgeneral](https://social.msdn.microsoft.com/Forums/vstudio/en-US/4f8ac9a7-1c80-4294-8d8e-8e233bf8c521/c-console-app-to-monitor-a-process-and-its-cpu?forum=csharpgeneral)
* [https://inov8.wordpress.com/2010/08/29/c-asp-net-creating-an-asynchronous-threaded-worker-process-manager-and-updatepanel-progress-monitor-control/](https://inov8.wordpress.com/2010/08/29/c-asp-net-creating-an-asynchronous-threaded-worker-process-manager-and-updatepanel-progress-monitor-control/)
* [https://www.romaniancoder.com/pmonitor/](https://www.romaniancoder.com/pmonitor/)
	* [https://github.com/dangeabunea/PMonitor](https://github.com/dangeabunea/PMonitor)
	* [http://www.dotnetframework.org/default.aspx/WCF/WCF/3@5@30729@1/untmp/Orcas/SP/ndp/cdf/src/WCF/infocard/Service/managed/Microsoft/InfoCards/ProcessMonitor@cs/1/ProcessMonitor@cs](http://www.dotnetframework.org/default.aspx/WCF/WCF/3@5@30729@1/untmp/Orcas/SP/ndp/cdf/src/WCF/infocard/Service/managed/Microsoft/InfoCards/ProcessMonitor@cs/1/ProcessMonitor@cs)

## ServiceStack

* Home: [https://servicestack.net/](https://servicestack.net/)
* Doc: [http://docs.servicestack.net/](http://docs.servicestack.net/)
* GitHub: [https://github.com/ServiceStack](https://github.com/ServiceStack)
* RabbitMQ:
	* Doc: [http://docs.servicestack.net/rabbit-mq](http://docs.servicestack.net/rabbit-mq)
	* Setup: [https://github.com/ServiceStack/rabbitmq-windows](https://github.com/ServiceStack/rabbitmq-windows)
	* [https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Server.Tests/Messaging/MqServerIntroTests.cs](https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Server.Tests/Messaging/MqServerIntroTests.cs)
	* [https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Common.Tests/Messaging/MqServerAppHostTests.cs](https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Common.Tests/Messaging/MqServerAppHostTests.cs)
	* [https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Common.Tests/Messaging/RabbitMqTests.cs](https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Common.Tests/Messaging/RabbitMqTests.cs)

## Messaging

* retlang: [https://github.com/Hades32/retlang](https://github.com/Hades32/retlang)
* fibrous: [https://github.com/chrisa23/Fibrous](https://github.com/chrisa23/Fibrous)
* Rx: [http://reactivex.io/](http://reactivex.io/)
	* [http://rxmarbles.com/#debounce](http://rxmarbles.com/#debounce)

### RabbitMQ

* Microservices with C# and RabbitMQ
	* [https://insidethecpu.com/2015/05/22/microservices-with-c-and-rabbitmq/](https://insidethecpu.com/2015/05/22/microservices-with-c-and-rabbitmq/)
	* [https://insidethecpu.com/2015/07/17/microservices-in-c-part-1-building-and-testing/](https://insidethecpu.com/2015/07/17/microservices-in-c-part-1-building-and-testing/)
	* [https://insidethecpu.com/2015/07/31/microservices-in-c-part-2-consistent-message-delivery/](https://insidethecpu.com/2015/07/31/microservices-in-c-part-2-consistent-message-delivery/)

## Actor frameworks

* [https://arxiv.org/abs/1505.07368](https://arxiv.org/abs/1505.07368)
	* [https://actor-framework.org/pdf/chs-rapc-16.pdf](https://actor-framework.org/pdf/chs-rapc-16.pdf)

### Microsoft Orleans

### Microsoft Service Fabric

### Akka

### Akka.NET

### proto.actor

* [http://proto.actor/](http://proto.actor/)
* C++ port (very very early stage...): [https://github.com/whitglint/protoactor-cpp](https://github.com/whitglint/protoactor-cpp)
* Uses gRPC; X-platform: .NET, Java, Go
* Similar to Orleans (virtual actors: Grains)

### CAF

* [http://actor-framework.org/](http://actor-framework.org/)
* [https://actor-framework.readthedocs.io/en/stable/](https://actor-framework.readthedocs.io/en/stable/)
* [https://github.com/actor-framework/actor-framework](https://github.com/actor-framework/actor-framework)
* [http://matthias.vallentin.net/slides/caf-rise.pdf](http://matthias.vallentin.net/slides/caf-rise.pdf)

###Interop

* CAF: NO [https://groups.google.com/forum/#!topic/actor-framework/-SsSiLdTico](https://groups.google.com/forum/#!topic/actor-framework/-SsSiLdTico)
* Akka Artery (Experimental, UDP): [https://doc.akka.io/docs/akka/2.4/scala/remoting-artery.html](https://doc.akka.io/docs/akka/2.4/scala/remoting-artery.html)
	* Based on Aeron (UDP *cast Java lib with Java/C++/.NET clients) : [https://github.com/real-logic/Aeron](https://github.com/real-logic/Aeron)
* [https://github.com/AsynkronIT/protoactor-go/issues/35](https://github.com/AsynkronIT/protoactor-go/issues/35)