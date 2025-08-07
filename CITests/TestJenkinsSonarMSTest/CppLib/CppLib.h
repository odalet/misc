// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the CPPLIB_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// CPPLIB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CPPLIB_EXPORTS
#define CPPLIB_API __declspec(dllexport)
#else
#define CPPLIB_API __declspec(dllimport)
#endif

// This class is exported from the CppLib.dll
class CPPLIB_API CCppLib {
public:
	CCppLib(void);
	// TODO: add your methods here.
};

extern CPPLIB_API int nCppLib;

CPPLIB_API int fnCppLib(void);
