# Visual Studio + Linux, WSL tips

## WSL

### Offline installs

* LxRunOffline (no Microsoft's Ubuntu 18.04 this way...):
	* [https://github.com/DDoSolitary/LxRunOffline](https://github.com/DDoSolitary/LxRunOffline)
	* [https://github.com/DDoSolitary/LxRunOffline/wiki](https://github.com/DDoSolitary/LxRunOffline/wiki)
	* [https://github.com/DDoSolitary/LxRunOffline/wiki/Creating-shortcuts-to-installations](https://github.com/DDoSolitary/LxRunOffline/wiki/Creating-shortcuts-to-installations)
* Offline Appx packages:
	* For Windows Server (No Ubuntu 18.04): [https://docs.microsoft.com/en-us/windows/wsl/install-on-server](https://docs.microsoft.com/en-us/windows/wsl/install-on-server) 
	* 18.04: [https://www.reddit.com/r/bashonubuntuonwindows/comments/8kgkn8/possible_to_grab_ubuntu_1804_appx_from_microsoft/?st=ji3cqc75&sh=a056ca9b](https://www.reddit.com/r/bashonubuntuonwindows/comments/8kgkn8/possible_to_grab_ubuntu_1804_appx_from_microsoft/?st=ji3cqc75&sh=a056ca9b) --> [https://drive.google.com/file/d/13WOCb8jR0-YIYXR-ocufKJ529PPg0IC4/view](https://drive.google.com/file/d/13WOCb8jR0-YIYXR-ocufKJ529PPg0IC4/view)

### Development tools

* [https://blogs.msdn.microsoft.com/vcblog/2017/02/08/targeting-windows-subsystem-for-linux-from-visual-studio/](https://blogs.msdn.microsoft.com/vcblog/2017/02/08/targeting-windows-subsystem-for-linux-from-visual-studio/)


On Ubuntu 18.04; installed:

* build-essential
* cmake

See below:

    $ sudo apt install -y build-essential
    $ gcc --version
    	gcc (Ubuntu 7.3.0-16ubuntu3) 7.3.0
		Copyright (C) 2017 Free Software Foundation, Inc.
		This is free software; see the source for copying conditions.  There is NO
		warranty; not even for MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
	$ sudo apt install -y gdbserver
	$ sudo apt install -y cmake
	$ cmake --version
		cmake version 3.10.2
	$ sudo apt install -y openssh-server
	$ sudo nano /etc/ssh/sshd_config
		--> make sure PasswordAuthentication is yes
	$ sudo ssh-keygen -A

* .NET Core (See [https://www.microsoft.com/net/learn/get-started/linux/ubuntu18-04](https://www.microsoft.com/net/learn/get-started/linux/ubuntu18-04))


#### VSCode

See [https://nickjanetakis.com/blog/using-wsl-and-mobaxterm-to-create-a-linux-dev-environment-on-windows](https://nickjanetakis.com/blog/using-wsl-and-mobaxterm-to-create-a-linux-dev-environment-on-windows)

First installed x11-apps to test the X11 server in mobaxterm

	# Download the Linux .deb package from: https://code.visualstudio.com/download
	
	sudo apt-get install libgtk2.0-0 libxss1 libasound2
	sudo dpkg -i <the_file_you_just_downloaded>.deb
	sudo apt-get install -f 

This does not work... Dependencies are missing:

	$ sudo dpkg -i code_1.24.1-1528912196_amd64.deb
	Selecting previously unselected package code.
	(Reading database ... 52408 files and directories currently installed.)
	Preparing to unpack code_1.24.1-1528912196_amd64.deb ...
	Unpacking code (1.24.1-1528912196) ...
	dpkg: dependency problems prevent configuration of code:
	 code depends on libnotify4; however:
	  Package libnotify4 is not installed.
	 code depends on libnss3; however:
	  Package libnss3 is not installed.
	 code depends on libgconf-2-4; however:
	  Package libgconf-2-4 is not installed.
	 code depends on libsecret-1-0; however:
	  Package libsecret-1-0 is not installed.
	
	dpkg: error processing package code (--install):
	 dependency problems - leaving unconfigured
	Processing triggers for mime-support (3.60ubuntu1) ...
	Errors were encountered while processing:
	 code
 
Fixing: 

	$ sudo apt --fix-broken install
	Reading package lists... Done
	Building dependency tree
	Reading state information... Done
	Correcting dependencies... Done
	The following additional packages will be installed:
	  at-spi2-core dconf-gsettings-backend dconf-service gconf-service gconf-service-backend gconf2-common glib-networking glib-networking-common
	  glib-networking-services gsettings-desktop-schemas libatk-bridge2.0-0 libatspi2.0-0 libcairo-gobject2 libcolord2 libdbus-glib-1-2 libdconf1
	  libegl-mesa0 libegl1 libepoxy0 libgbm1 libgconf-2-4 libglapi-mesa libglvnd0 libgtk-3-0 libgtk-3-bin libgtk-3-common libjson-glib-1.0-0
	  libjson-glib-1.0-common liblcms2-2 libnotify4 libnspr4 libnss3 libproxy1v5 librest-0.7-0 libsecret-1-0 libsecret-common libsoup-gnome2.4-1
	  libsoup2.4-1 libwayland-client0 libwayland-cursor0 libwayland-egl1-mesa libwayland-server0 libx11-xcb1 libxcb-dri2-0 libxcb-dri3-0
	  libxcb-present0 libxcb-sync1 libxcb-xfixes0 libxkbcommon0 libxshmfence1 libxtst6 notification-daemon
	Suggested packages:
	  colord gvfs liblcms2-utils
	The following NEW packages will be installed:
	  at-spi2-core dconf-gsettings-backend dconf-service gconf-service gconf-service-backend gconf2-common glib-networking glib-networking-common
	  glib-networking-services gsettings-desktop-schemas libatk-bridge2.0-0 libatspi2.0-0 libcairo-gobject2 libcolord2 libdbus-glib-1-2 libdconf1
	  libegl-mesa0 libegl1 libepoxy0 libgbm1 libgconf-2-4 libglapi-mesa libglvnd0 libgtk-3-0 libgtk-3-bin libgtk-3-common libjson-glib-1.0-0
	  libjson-glib-1.0-common liblcms2-2 libnotify4 libnspr4 libnss3 libproxy1v5 librest-0.7-0 libsecret-1-0 libsecret-common libsoup-gnome2.4-1
	  libsoup2.4-1 libwayland-client0 libwayland-cursor0 libwayland-egl1-mesa libwayland-server0 libx11-xcb1 libxcb-dri2-0 libxcb-dri3-0
	  libxcb-present0 libxcb-sync1 libxcb-xfixes0 libxkbcommon0 libxshmfence1 libxtst6 notification-daemon
	0 upgraded, 52 newly installed, 0 to remove and 99 not upgraded.
	1 not fully installed or removed.
	Need to get 6,763 kB of archives.
	After this operation, 30.4 MB of additional disk space will be used.
	Do you want to continue? [Y/n]

**It works!**

### Qt

* [https://doc.ubuntu-fr.org/qt](https://doc.ubuntu-fr.org/qt)
* [https://wiki.qt.io/Install_Qt_5_on_Ubuntu](https://wiki.qt.io/Install_Qt_5_on_Ubuntu): Replacing 5.7 with 5.11
* [https://www.ics.com/blog/getting-started-qt-and-qt-creator-linux](https://www.ics.com/blog/getting-started-qt-and-qt-creator-linux)
* [https://bugs.launchpad.net/ubuntu/+source/qtcreator/+bug/566387](https://bugs.launchpad.net/ubuntu/+source/qtcreator/+bug/566387)

download:

	$ wget http://download.qt.io/official_releases/qt/5.11/5.11.1/qt-opensource-linux-x64-5.11.1.run

### Other tools

* xterm
* konsole

### Docker

[https://nickjanetakis.com/blog/setting-up-docker-for-windows-and-wsl-to-work-flawlessly](https://nickjanetakis.com/blog/setting-up-docker-for-windows-and-wsl-to-work-flawlessly)

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