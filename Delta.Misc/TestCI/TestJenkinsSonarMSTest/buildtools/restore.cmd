@echo off

set solution=%~dp0..\TestJenkinsSonarMSTest.sln

%~dp0.\nuget.exe restore %solution% -Source http://localhost:8081/nexus/service/local/nuget/nuget-all/ -FallbackSource https://www.nuget.org/api/v2/
