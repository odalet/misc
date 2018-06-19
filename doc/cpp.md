# C++

## Language

* const usage
	* [http://duramecho.com/ComputerInformation/WhyHowCppConst.html](http://duramecho.com/ComputerInformation/WhyHowCppConst.html)
	* `const T* foo` = `T const * foo` != * `T* const foo`
	* `const T& foo` = `T const & foo` != * `T& const foo`


* move Semantics
	* [http://thbecker.net/articles/rvalue_references/section_01.html](http://thbecker.net/articles/rvalue_references/section_01.html)
	* [http://www.open-std.org/jtc1/sc22/wg21/docs/papers/2006/n2027.html#Move_Semantics](http://www.open-std.org/jtc1/sc22/wg21/docs/papers/2006/n2027.html#Move_Semantics)


## Compilation

* Exporting symbols ins shared libraries
	* [https://stackoverflow.com/questions/2164827/explicitly-exporting-shared-library-functions-in-linux](https://stackoverflow.com/questions/2164827/explicitly-exporting-shared-library-functions-in-linux)
	* **[https://gcc.gnu.org/wiki/Visibility](https://gcc.gnu.org/wiki/Visibility)**
	* [http://gernotklingler.com/blog/creating-using-shared-libraries-different-compilers-different-operating-systems/](http://gernotklingler.com/blog/creating-using-shared-libraries-different-compilers-different-operating-systems/)

## Portability

* Minimal "portable" cmake project that builds and links a shared library. Builds on various operating systems with various compilers
	* [https://github.com/gklingler/sharedLibsDemo](https://github.com/gklingler/sharedLibsDemo)
	* [http://gernotklingler.com/blog/creating-using-shared-libraries-different-compilers-different-operating-systems/](http://gernotklingler.com/blog/creating-using-shared-libraries-different-compilers-different-operating-systems/)
* Binary Compatible C++ interfaces
	* [https://chadaustin.me/cppinterface.html](https://chadaustin.me/cppinterface.html)

## Formatting & coding rules

* clang-format and QtCreator: [https://github.com/qt-creator/qt-creator/blob/master/dist/clangformat/README.md](https://github.com/qt-creator/qt-creator/blob/master/dist/clangformat/README.md)
