# Service Wrappers & multi-process

Idées :

* Stack technique (.NET) :
	* Warden: [https://github.com/warden-stack/Warden](https://github.com/warden-stack/Warden)
	* Topshelf: [http://topshelf-project.com/](http://topshelf-project.com/)
	* Serilog: [https://serilog.net/](https://serilog.net/) 
	* .NET Standard
		* Service Windows --> .NET 4.6.2
		* Service Linux --> .NET Core
		* Client Console --> .NET Core / .NET 4.6.2
* Lib (.NET Standard) de lancement / monitoring de processes --> Warden
* Appli console "serveur" : .NET Core ou 4.6.2
* Service Windows "serveur" : .NET 4.6.2
* Service Linux "serveur" : .NET Core - appel de l'app console via **init.d**, **systemd**; see:
	* [https://logankpaschke.com/linux/systemd/dotnet/systemd-dotnet-1/](https://logankpaschke.com/linux/systemd/dotnet/systemd-dotnet-1/)
	* [https://developers.redhat.com/blog/2017/06/07/writing-a-linux-daemon-in-c/](https://developers.redhat.com/blog/2017/06/07/writing-a-linux-daemon-in-c/)
	* [http://pmcgrath.net/running-a-simple-dotnet-core-linux-daemon](http://pmcgrath.net/running-a-simple-dotnet-core-linux-daemon)
* Appli console "client"
	* un seul exe pour le client et le serveur console (voir le service via Topshelf)
	* utiliser des flags ou de la détection auto pour savoir si on se lance en mode serveur ou client
* Logging : Serilog? Connector?
* Monitoring: 
	* API exposed by the server; can be consumed by:
		* Console client
		* Web app
		* HTTP API? Protobuf?
	* Based on Warden Web Panel? 

## Native Service Wrappers (used by Java apps)

* Apache: [https://commons.apache.org/proper/commons-daemon/procrun.html](https://commons.apache.org/proper/commons-daemon/procrun.html) - Used by:
	* Artifactory,
	* Atlassian suite (cf. tomcat8.exe)
* Tanuki: [https://wrapper.tanukisoftware.com/doc/english/introduction.html](https://wrapper.tanukisoftware.com/doc/english/introduction.html) - Used by:
	* Sonarqube
	* Nexus
* Yajsw: 
	* [http://yajsw.sourceforge.net/](http://yajsw.sourceforge.net/)
	* [https://github.com/cstroe/yajsw](https://github.com/cstroe/yajsw)

## .Net Service Wrappers

* Cloudbees (Jenkins): [https://github.com/kohsuke/winsw](https://github.com/kohsuke/winsw)
* Nginx: [https://github.com/daptiv/NginxService](https://github.com/daptiv/NginxService)


# Serialization

* [https://thrift.apache.org/](https://thrift.apache.org/)
* [http://www.eprosima.com/index.php/resources-all/performance/apache-thrift-vs-protocol-buffers-vs-fast-buffers](http://www.eprosima.com/index.php/resources-all/performance/apache-thrift-vs-protocol-buffers-vs-fast-buffers)
* [https://stackoverflow.com/questions/69316/biggest-differences-of-thrift-vs-protocol-buffers](https://stackoverflow.com/questions/69316/biggest-differences-of-thrift-vs-protocol-buffers)
* [https://stackoverflow.com/questions/7390561/zeromq-protocol-buffers](https://stackoverflow.com/questions/7390561/zeromq-protocol-buffers)
	* [https://github.com/eishay/jvm-serializers/wiki](https://github.com/eishay/jvm-serializers/wiki)
	* [https://www.dotkam.com/2011/09/09/zeromq-and-google-protocol-buffers/](https://www.dotkam.com/2011/09/09/zeromq-and-google-protocol-buffers/)

# Low-level programming languages

## Rust

## Others

* C2: [http://c2lang.org/site/](http://c2lang.org/site/)
* Nim: [https://nim-lang.org/](https://nim-lang.org/)
* Jai: 
	* [https://inductive.no/jai/](https://inductive.no/jai/)
	* [https://github.com/BSVino/JaiPrimer/blob/master/JaiPrimer.md](https://github.com/BSVino/JaiPrimer/blob/master/JaiPrimer.md) 
	* [http://www.mrphilgames.com/jai/](http://www.mrphilgames.com/jai/)
* Rithie/Rix: [https://github.com/riolet/rix](https://github.com/riolet/rix)

# CI / C++ / Linux...

* [https://jenkins.io/blog/2017/07/07/jenkins-conan/](https://jenkins.io/blog/2017/07/07/jenkins-conan/)
* [http://nuclear.mutantstargoat.com/articles/make/#installing-libraries](http://nuclear.mutantstargoat.com/articles/make/#installing-libraries)
* [https://spin.atomicobject.com/2016/08/26/makefile-c-projects/](https://spin.atomicobject.com/2016/08/26/makefile-c-projects/)
* CMake
	* Using CMake to handle 3rd-party: [https://www.selectiveintellect.net/blog/2016/7/29/using-cmake-to-add-third-party-libraries-to-your-project-1](https://www.selectiveintellect.net/blog/2016/7/29/using-cmake-to-add-third-party-libraries-to-your-project-1)
	* [https://stackoverflow.com/questions/8153519/how-to-automatically-download-c-dependencies-in-a-cross-platform-way-cmake](https://stackoverflow.com/questions/8153519/how-to-automatically-download-c-dependencies-in-a-cross-platform-way-cmake)
	* [https://www.guyrutenberg.com/2012/07/19/auto-detect-dependencies-when-building-debs-using-cmake/](https://www.guyrutenberg.com/2012/07/19/auto-detect-dependencies-when-building-debs-using-cmake/)
	* [https://stackoverflow.com/questions/41251474/how-to-import-zeromq-libraries-in-cmake](https://stackoverflow.com/questions/41251474/how-to-import-zeromq-libraries-in-cmake)
	* [http://voices.canonical.com/jussi.pakkanen/2013/03/26/a-list-of-common-cmake-antipatterns/](http://voices.canonical.com/jussi.pakkanen/2013/03/26/a-list-of-common-cmake-antipatterns/)
	* **[https://rix0r.nl/blog/2015/08/13/cmake-guide/](https://rix0r.nl/blog/2015/08/13/cmake-guide/)** 
	* [http://cgold.readthedocs.io/en/latest/index.html](http://cgold.readthedocs.io/en/latest/index.html) 
* pkg-config: [https://en.wikipedia.org/wiki/Pkg-config](https://en.wikipedia.org/wiki/Pkg-config)
* Hunter: [https://github.com/ruslo/hunter](https://github.com/ruslo/hunter)
* [http://gazebosim.org/tutorials?tut=install_dependencies_from_source](http://gazebosim.org/tutorials?tut=install_dependencies_from_source)
* In-source vs out-of-source builds:
	* [http://cgold.readthedocs.io/en/latest/tutorials/out-of-source.html](http://cgold.readthedocs.io/en/latest/tutorials/out-of-source.html)
	* [http://voices.canonical.com/jussi.pakkanen/2013/04/16/why-you-should-consider-using-separate-build-directories/](http://voices.canonical.com/jussi.pakkanen/2013/04/16/why-you-should-consider-using-separate-build-directories/)
* Other Links:
	* [https://www.reddit.com/r/cpp/comments/6nu39i/approaches_to_c_dependency_management_or_why_we/](https://www.reddit.com/r/cpp/comments/6nu39i/approaches_to_c_dependency_management_or_why_we/)
	* [https://medium.com/@sdboyer/so-you-want-to-write-a-package-manager-4ae9c17d9527](https://medium.com/@sdboyer/so-you-want-to-write-a-package-manager-4ae9c17d9527)
	* [https://github.com/ruslo/hunter](https://github.com/ruslo/hunter)
[https://docs.hunter.sh/en/latest/packages.html](https://docs.hunter.sh/en/latest/packages.html)
	* [https://hackernoon.com/approaches-to-c-dependency-management-or-why-we-built-buckaroo-26049d4646e7](https://hackernoon.com/approaches-to-c-dependency-management-or-why-we-built-buckaroo-26049d4646e7)
	* [http://blog.conan.io/2016/03/30/are-c-and-c++-languages-ready-for-the-npm-debacle.html](http://blog.conan.io/2016/03/30/are-c-and-c++-languages-ready-for-the-npm-debacle.html)

# How to unit test memory leaks?

* **dotMemory Unit**: [https://www.jetbrains.com/dotmemory/unit/](https://www.jetbrains.com/dotmemory/unit/)
* [https://stackoverflow.com/questions/3652028/unit-testing-memory-leaks](https://stackoverflow.com/questions/3652028/unit-testing-memory-leaks)
* [https://stackoverflow.com/questions/578967/how-can-i-write-a-unit-test-to-determine-whether-an-object-can-be-garbage-collec](https://stackoverflow.com/questions/578967/how-can-i-write-a-unit-test-to-determine-whether-an-object-can-be-garbage-collec)
* Keeping VS2017 Build Tools up-to-date: [https://alastaircrabtree.com/keeping-visual-studio-2017-build-tools-up-to-date/](https://alastaircrabtree.com/keeping-visual-studio-2017-build-tools-up-to-date/)


```

dotMemoryUnit.exe "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" -- "C:\Users\odalet\Documents\Visual Studio 2017\Projects\ConsoleApp4\MemTests\bin\Debug\MemTests.dll" /InIsolation /Settings:"C:\Users\odalet\Documents\Visual Studio 2017\Projects\ConsoleApp4\x64.runsettings" /TestAdapterPath:C:\WORK\REPOSITORIES\addup-cameras\buildtools\MsTest\Adapter /logger:Console                   

dotMemory Unit 3.0 part of 2017.3  build 111.0.20171219.95427. Copyright (C) 2015-2017 JetBrains s.r.o.                                                                                                                                       
Outil en ligne de commande d'exécution de tests Microsoft (R), version 15.5.0                                                                                                                                                                 
Copyright (c) Microsoft Corporation. Tous droits réservés.                                                                                                                                                                                    
                                                                                                                                                                                                                                              
Démarrage de l'exécution de tests, patientez...                                                                                                                                                                                               
La série de tests utilise des DLL générées pour le framework .NETFramework,Version=v4.5 et la plateforme X64. Les DLL suivantes ne font pas partie de la série de tests :                                                                     
MemTests.dll est généré pour le framework 4.6.2 et la plateforme X86.                                                                                                                                                                         
Accédez à http://go.microsoft.com/fwlink/?LinkID=236877&clcid=0x409 pour plus de détails sur la gestion de ces paramètres.                                                                                                                    
                                                                                                                                                                                                                                              
Réussi(s) : Fake                                                                                                                                                                                                                              
Messages de sortie standard :                                                                                                                                                                                                                 
                                                                                                                                                                                                                                              
                                                                                                                                                                                                                                              
Debug Trace:                                                                                                                                                                                                                                  
TRACE                                                                                                                                                                                                                                         
 DEBUG                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                              
Messages d'erreur standard :                                                                                                                                                                                                                  
 CON                                                                                                                                                                                                                                          
                                                                                                                                                                                                                                              
Réussi(s) : Foo                                                                                                                                                                                                                               
Messages de sortie standard :                                                                                                                                                                                                                 
                                                                                                                                                                                                                                              
                                                                                                                                                                                                                                              
Debug Trace:                                                                                                                                                                                                                                  
Size: 0,0234375                                                                                                                                                                                                                               
                                                                                                                                                                                                                                              
                                                                                                                                                                                                                                              
Nombre total de tests : 2. Réussis : 2. Non réussis : 0. Ignorés : 0.                                                                                                                                                                         
Série de tests réussie.                                                                                                                                                                                                                       
Durée d'exécution des tests : 11,4389 Secondes 

```

**Tested from `C:\Users\odalet\Documents\Visual Studio 2017\Projects\ConsoleApp4\packages\JetBrains.dotMemoryUnit.3.0.20171219.105559\lib\tools`**


# Misc

* Single-file C libraries: [https://github.com/nothings/stb](https://github.com/nothings/stb)

* **VS Build Tools: [https://alastaircrabtree.com/keeping-visual-studio-2017-build-tools-up-to-date/](https://alastaircrabtree.com/keeping-visual-studio-2017-build-tools-up-to-date/)**
