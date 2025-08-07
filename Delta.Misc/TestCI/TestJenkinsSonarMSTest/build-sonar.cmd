pushd %~dp0
cd %~dp0

rd /q /s "%~dp0.sonarqube"

C:\JAVA\JENKINS\tools\hudson.plugins.sonar.MsBuildSQRunnerInstallation\MSBuild_SornarQube_Runner_1.1\MSBuild.SonarQube.Runner.exe begin /k:TestJenkinsSonarMSTest /n:TestJenkinsSonarMSTest /v:1.0 /d:sonar.host.url=http://localhost:9000

"C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\MSBuild.exe" "%~dp0.\TestJenkinsSonarMSTest.sln"

C:\JAVA\JENKINS\tools\hudson.plugins.sonar.MsBuildSQRunnerInstallation\MSBuild_SornarQube_Runner_1.1\MSBuild.SonarQube.Runner.exe end

popd