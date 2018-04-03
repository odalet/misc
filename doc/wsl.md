# Visual Studio + Linux, WSL tips

## VS + Linux

* [https://blogs.msdn.microsoft.com/vcblog/2017/04/14/bring-your-existing-c-linux-projects-to-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2017/04/14/bring-your-existing-c-linux-projects-to-visual-studio/)
* [https://blogs.msdn.microsoft.com/vcblog/2017/04/11/linux-development-with-c-in-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2017/04/11/linux-development-with-c-in-visual-studio/)
* [https://blogs.msdn.microsoft.com/vcblog/2017/03/07/use-any-c-compiler-with-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2017/03/07/use-any-c-compiler-with-visual-studio/)
* [https://blogs.msdn.microsoft.com/vcblog/2017/02/22/learn-c-concepts-with-visual-studio-and-the-wsl/](https://blogs.msdn.microsoft.com/vcblog/2017/02/22/learn-c-concepts-with-visual-studio-and-the-wsl/)
* [https://blogs.msdn.microsoft.com/vcblog/2017/02/08/targeting-windows-subsystem-for-linux-from-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2017/02/08/targeting-windows-subsystem-for-linux-from-visual-studio/)
* [https://blogs.msdn.microsoft.com/vcblog/2016/03/30/visual-c-for-linux-development/](https://blogs.msdn.microsoft.com/vcblog/2016/03/30/visual-c-for-linux-development/)
* [https://github.com/robotdad/vclinux](https://github.com/robotdad/vclinux)* 
[https://github.com/robotdad/LinuxCrossCompile](https://github.com/robotdad/LinuxCrossCompile)
* [https://askubuntu.com/questions/410244/a-command-to-list-all-users-and-how-to-add-delete-modify-users](https://askubuntu.com/questions/410244/a-command-to-list-all-users-and-how-to-add-delete-modify-users)
* [https://stackoverflow.com/questions/44088855/connecting-to-the-windows-subsystem-for-linux-from-visual-studio-2017](https://stackoverflow.com/questions/44088855/connecting-to-the-windows-subsystem-for-linux-from-visual-studio-2017)
* [https://www.hanselman.com/blog/WritingAndDebuggingLinuxCApplicationsFromVisualStudioUsingTheWindowsSubsystemForLinux.aspx](https://www.hanselman.com/blog/WritingAndDebuggingLinuxCApplicationsFromVisualStudioUsingTheWindowsSubsystemForLinux.aspx)


### CMake

* [https://blogs.msdn.microsoft.com/vcblog/2017/08/25/visual-c-for-linux-development-with-cmake/](https://blogs.msdn.microsoft.com/vcblog/2017/08/25/visual-c-for-linux-development-with-cmake/)
* [https://blogs.msdn.microsoft.com/vcblog/2016/10/05/cmake-support-in-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2016/10/05/cmake-support-in-visual-studio/)
* [https://blogs.msdn.microsoft.com/vcblog/2016/10/05/bring-your-c-codebase-to-visual-studio-with-open-folder/](https://blogs.msdn.microsoft.com/vcblog/2016/10/05/bring-your-c-codebase-to-visual-studio-with-open-folder/)
* [https://askubuntu.com/questions/355565/how-do-i-install-the-latest-version-of-cmake-from-the-command-line](https://askubuntu.com/questions/355565/how-do-i-install-the-latest-version-of-cmake-from-the-command-line)
* [https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes)



## Git
### Have Git show correct status

If `git status` shows all files as modified...

See [https://github.com/Microsoft/WSL/issues/184](https://github.com/Microsoft/WSL/issues/184)

TLDR: 

	git config core.autocrlf true
	git config core.filemode false

Beware: `autocrlf` might be dangerous for repos shared between Windows / Linux; however, if not set, each time one issues `git status` (as is done by the bash prompt extension), git outputs warnings about CRLF being changed to LF... 

### Upgrade Git

See [https://askubuntu.com/questions/579589/upgrade-git-version-on-ubuntu-14-04](https://askubuntu.com/questions/579589/upgrade-git-version-on-ubuntu-14-04)

TLDR: 

	sudo add-apt-repository ppa:git-core/ppa
	sudo apt-get update
	sudo apt-get install git

### Configure Git diff tool 

This allows to associate a Windows-gui diff/merge tool with Linux's git.

See [https://www.sep.com/sep-blog/2017/06/07/20170607wsl-git-and-beyond-compare/](https://www.sep.com/sep-blog/2017/06/07/20170607wsl-git-and-beyond-compare/)

# Misc (non-WSL)

## Enable Windows Console to display VT100 escape codes (colors)

Only works in Windows 10...

	Windows Registry Editor Version 5.00
	
	[HKEY_CURRENT_USER\Console]
	"VirtualTerminalLevel"=dword:00000001

## Setup Rust Debugging in VS Code

[http://www.brycevandyk.com/debug-rust-on-windows-with-visual-studio-code-and-the-msvc-debugger/](http://www.brycevandyk.com/debug-rust-on-windows-with-visual-studio-code-and-the-msvc-debugger/) 