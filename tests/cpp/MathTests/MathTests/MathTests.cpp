// MathTests.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "SquareMatrix.h"

using namespace MathTests;

template<typename T, size_t N>
void dump(const Vector<T, N>& v) {
	std::cout << "[ ";
	for (auto i = 0; i < N; i++)
		std::cout << v[i] << " ";
	std::cout << "]" << std::endl;
}

template<typename T, size_t N>
void dump(const SquareMatrix<T, N>& m) {
	for (auto i = 0; i < N; i++)
		dump(m[i]);
}

int main()
{
	Vector<float, 4> v1{ 1, 2, 3, 4 };
	Vector<float, 4> v2{ 4, 5, 6, 7 };

	auto v = v1;
	v += { 10, 0, 0, 0};
	dump(v);
	dump(v1);
	dump(v2);
	dump(v1 + v2);

	std::cout << "-------------------------" << std::endl;
	SquareMatrix<float, 4> m1;

	auto m2 = m1; // copy?

	m1[0][3] = 3;
	m2[3][0] = 3;

	try {
		auto& i = m1.at(5, 5);
	}
	catch (const std::exception& ex) {
		std::cout << "ERROR: " << ex.what() << std::endl;
	}
	std::cout << "-------------------------" << std::endl;

	dump(m1);
	std::cout << "-------------------------" << std::endl;
	dump(m2);
	std::cout << "-------------------------" << std::endl;

	m1 += m2;
	dump(m1);
	std::cout << "-------------------------" << std::endl;

	auto trans = m1.extractTranslation();
	dump(m1.row(3));
	dump(m1.column(3));
	std::cout << "Translation: ";
	dump(trans);
	std::cout << "-------------------------" << std::endl;

	getchar();
	return 0;
}

