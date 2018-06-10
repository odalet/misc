# Process Management

## Watchers

* Circus (python): [http://circus.readthedocs.io/en/latest/](http://circus.readthedocs.io/en/latest/)
* Supervisord (no Windows): [http://supervisord.org/index.html](http://supervisord.org/index.html)

## Gracefully terminating a process

### Windows

* [https://stackoverflow.com/questions/813086/can-i-send-a-ctrl-c-sigint-to-an-application-on-windows](https://stackoverflow.com/questions/813086/can-i-send-a-ctrl-c-sigint-to-an-application-on-windows)
* [https://stackoverflow.com/questions/1679876/atexit-exit-delegate-in-c-sharp](https://stackoverflow.com/questions/1679876/atexit-exit-delegate-in-c-sharp)
* [https://stackoverflow.com/questions/177856/how-do-i-trap-ctrl-c-in-a-c-sharp-console-app](https://stackoverflow.com/questions/177856/how-do-i-trap-ctrl-c-in-a-c-sharp-console-app)
* [https://docs.microsoft.com/en-us/cpp/c-runtime-library/reference/signal](https://docs.microsoft.com/en-us/cpp/c-runtime-library/reference/signal)
	* [https://docs.microsoft.com/en-us/cpp/c-runtime-library/process-and-environment-control](https://docs.microsoft.com/en-us/cpp/c-runtime-library/process-and-environment-control)
* > taskkill
* Microsoft's Process Extensions (from ASP.NET Core): [https://github.com/aspnet/Common/blob/ffb7c20fb22a31ac31d3a836a8455655867e8e16/shared/Microsoft.Extensions.Process.Sources/ProcessHelper.cs#L22](https://github.com/aspnet/Common/blob/ffb7c20fb22a31ac31d3a836a8455655867e8e16/shared/Microsoft.Extensions.Process.Sources/ProcessHelper.cs#L22)
	* [https://github.com/aspnet/Common/pull/360](https://github.com/aspnet/Common/pull/360)
	* Prefer this impl: [https://github.com/aspnet/Common/blob/dev/shared/Microsoft.Extensions.Process.Sources/ProcessHelper.cs](https://github.com/aspnet/Common/blob/dev/shared/Microsoft.Extensions.Process.Sources/ProcessHelper.cs)

### Linux / .NET Core

* [https://stackoverflow.com/questions/38291567/killing-gracefully-a-net-core-daemon-running-on-linux](https://stackoverflow.com/questions/38291567/killing-gracefully-a-net-core-daemon-running-on-linux)
* [https://github.com/aspnet/Hosting/issues/870](https://github.com/aspnet/Hosting/issues/870)
	* [https://github.com/dotnet/coreclr/blob/7dfb2ed8614dea8c2fe757592436a38694e91ebc/src/vm/exceptionhandling.cpp](https://github.com/dotnet/coreclr/blob/7dfb2ed8614dea8c2fe757592436a38694e91ebc/src/vm/exceptionhandling.cpp)
* [https://github.com/aspnet/Hosting/issues/1329](https://github.com/aspnet/Hosting/issues/1329)

## Mongod

* [https://docs.mongodb.com/manual/tutorial/manage-mongodb-processes/](https://docs.mongodb.com/manual/tutorial/manage-mongodb-processes/)
* [https://docs.mongodb.com/manual/tutorial/install-mongodb-on-windows/#configure-a-windows-service-for-mongodb-community-edition](https://docs.mongodb.com/manual/tutorial/install-mongodb-on-windows/#configure-a-windows-service-for-mongodb-community-edition)
* [https://stackoverflow.com/questions/7973113/how-to-stop-mongodb-server-on-windows](https://stackoverflow.com/questions/7973113/how-to-stop-mongodb-server-on-windows)
* C#
	* [https://stackoverflow.com/questions/6312936/howto-gracefully-shutdown-mongod-in-roleentrypoint-onstop-method](https://stackoverflow.com/questions/6312936/howto-gracefully-shutdown-mongod-in-roleentrypoint-onstop-method)
	* [https://stackoverflow.com/questions/29979713/mongoserver-does-not-contain-a-definition-for-create](https://stackoverflow.com/questions/29979713/mongoserver-does-not-contain-a-definition-for-create)
	* [https://www.mongodb.com/blog/post/introducing-20-net-driver](https://www.mongodb.com/blog/post/introducing-20-net-driver)

## Linux Kill

	$ kill -l
	 1) SIGHUP       2) SIGINT       3) SIGQUIT      4) SIGILL       5) SIGTRAP
	 6) SIGABRT      7) SIGBUS       8) SIGFPE       9) SIGKILL     10) SIGUSR1
	11) SIGSEGV     12) SIGUSR2     13) SIGPIPE     14) SIGALRM     15) SIGTERM
	16) SIGSTKFLT   17) SIGCHLD     18) SIGCONT     19) SIGSTOP     20) SIGTSTP
	21) SIGTTIN     22) SIGTTOU     23) SIGURG      24) SIGXCPU     25) SIGXFSZ
	26) SIGVTALRM   27) SIGPROF     28) SIGWINCH    29) SIGIO       30) SIGPWR
	31) SIGSYS      34) SIGRTMIN    35) SIGRTMIN+1  36) SIGRTMIN+2  37) SIGRTMIN+3
	38) SIGRTMIN+4  39) SIGRTMIN+5  40) SIGRTMIN+6  41) SIGRTMIN+7  42) SIGRTMIN+8
	43) SIGRTMIN+9  44) SIGRTMIN+10 45) SIGRTMIN+11 46) SIGRTMIN+12 47) SIGRTMIN+13
	48) SIGRTMIN+14 49) SIGRTMIN+15 50) SIGRTMAX-14 51) SIGRTMAX-13 52) SIGRTMAX-12
	53) SIGRTMAX-11 54) SIGRTMAX-10 55) SIGRTMAX-9  56) SIGRTMAX-8  57) SIGRTMAX-7
	58) SIGRTMAX-6  59) SIGRTMAX-5  60) SIGRTMAX-4  61) SIGRTMAX-3  62) SIGRTMAX-2
	63) SIGRTMAX-1  64) SIGRTMAX

* SIGINT = 2
* SigQUIT = 3
* SIGKILL = 9
* SIGTERM = 15

See [https://askubuntu.com/questions/14155/what-to-do-when-ctrl-c-wont-kill-running-job](https://askubuntu.com/questions/14155/what-to-do-when-ctrl-c-wont-kill-running-job) 

----

* kill vs pkill...