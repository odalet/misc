pushd %~dp0
cd %~dp0

rd /q /s "%~dp0.sonarqube"
rd /q /s "%~dp0build\log"
mkdir "%~dp0build\log"

"%~dp0.\buildtools\sonar\MSBuild.SonarQube.Runner.exe" begin /k:TestJenkinsSonarMSTest /n:TestJenkinsSonarMSTest /v:1.0 /d:sonar.host.url=http://localhost:9000

"C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\MSBuild.exe" "%~dp0.\TestJenkinsSonarMSTest\TestJenkinsSonarMSTest.sln" /t:rebuild /p:Configuration=Debug;WarningLevel=4 /fileLogger /fileLoggerParameters:LogFile="%~dp0.\build\log\build.log";Verbosity=detailed;Encoding=UTF-8

"%~dp0.\buildtools\sonar\MSBuild.SonarQube.Runner.exe" end

popd