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

## Misc

* Matrix:
	* [https://codereview.stackexchange.com/questions/186317/short-square-matrix-class-in-c-using-an-array](https://codereview.stackexchange.com/questions/186317/short-square-matrix-class-in-c-using-an-array) 
	* [https://stackoverflow.com/questions/34828429/matrix-multiplication-algorithm](https://stackoverflow.com/questions/34828429/matrix-multiplication-algorithm)
* Constructor initializer list: 
	* [https://stackoverflow.com/questions/4057948/initializing-a-member-array-in-constructor-initializer](https://stackoverflow.com/questions/4057948/initializing-a-member-array-in-constructor-initializer)
	* [http://www.cplusplus.com/reference/initializer_list/initializer_list/](http://www.cplusplus.com/reference/initializer_list/initializer_list/)
	* [https://akrzemi1.wordpress.com/2016/07/07/the-cost-of-stdinitializer_list/](https://akrzemi1.wordpress.com/2016/07/07/the-cost-of-stdinitializer_list/)
	* [https://en.cppreference.com/w/cpp/language/initializer_list](https://en.cppreference.com/w/cpp/language/initializer_list)
* Operators overloading:
	* [https://arne-mertz.de/2015/01/operator-overloading-the-basics/](https://arne-mertz.de/2015/01/operator-overloading-the-basics/)
	* [https://arne-mertz.de/2015/01/operator-overloading-common-practice/](https://arne-mertz.de/2015/01/operator-overloading-common-practice/)
	* [https://stackoverflow.com/questions/2425906/operator-overloading-outside-class](https://stackoverflow.com/questions/2425906/operator-overloading-outside-class)
* try/throw/catch, exceptions: 
	* [https://msdn.microsoft.com/fr-fr/library/6dekhbbc.aspx](https://msdn.microsoft.com/fr-fr/library/6dekhbbc.aspx)
	* [http://en.cppreference.com/w/cpp/error/out_of_range](http://en.cppreference.com/w/cpp/error/out_of_range)
* to_string: [https://stackoverflow.com/questions/5590381/easiest-way-to-convert-int-to-string-in-chttps://stackoverflow.com/questions/5590381/easiest-way-to-convert-int-to-string-in-c](https://stackoverflow.com/questions/5590381/easiest-way-to-convert-int-to-string-in-chttps://stackoverflow.com/questions/5590381/easiest-way-to-convert-int-to-string-in-c)
* Template Restrictions + Type aliases:
	* [https://stackoverflow.com/questions/16976720/how-do-i-restrict-a-template-class-to-certain-built-in-types](https://stackoverflow.com/questions/16976720/how-do-i-restrict-a-template-class-to-certain-built-in-types)
	* [https://stackoverflow.com/questions/43013243/can-using-be-used-to-type-alias-an-array](https://stackoverflow.com/questions/43013243/can-using-be-used-to-type-alias-an-array)
	* [https://stackoverflow.com/questions/6622452/alias-template-specialisation](https://stackoverflow.com/questions/6622452/alias-template-specialisation)


