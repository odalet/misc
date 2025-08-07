// CppLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CppLib.h"


// This is an example of an exported variable
CPPLIB_API int nCppLib=0;

// This is an example of an exported function.
CPPLIB_API int fnCppLib(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see CppLib.h for the class definition
CCppLib::CCppLib()
{
    return;
}
