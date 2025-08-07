#include "stdafx.h"
#include "CppCalc.h"

char* waste = new char[1024];

void foo(int unused) {
	
}

double CppCalc::add(double a, double b) { return a + b; }
double CppCalc::sub(double a, double b) { return a - b; }
double CppCalc::mul(double a, double b) { return a * b; }
double CppCalc::div(double a, double b) { return a / b; }


CppCalc::CppCalc(unsigned unusedParameter) { }
CppCalc::CppCalc(){ }
CppCalc::~CppCalc() { }
