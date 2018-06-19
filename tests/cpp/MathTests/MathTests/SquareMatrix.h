#pragma once

#include <type_traits>
#include <stdexcept>
#include <array>
#include <string>
#include <initializer_list>

namespace MathTests
{
	// Vector

	template<typename T, size_t N>
	class Vector
	{
		// See https://stackoverflow.com/questions/16976720/how-do-i-restrict-a-template-class-to-certain-built-in-types
		static_assert(std::is_floating_point<T>::value, "Vector can only be instantiated with floating point types");

	public:
		Vector() {}
		Vector(std::initializer_list<T> list) {
			auto i = 0;
			for (auto& value : list) {
				if (i >= N)
					throw std::out_of_range("initialization list size must be < " + std::to_string(N));
				items[i] = value;
				i++;
			}
		}

		const T& at(const size_t index) const {
			if (index >= N) throw std::out_of_range("indices must be < " + std::to_string(N));
			return items[index];
		}

		T& operator[](const size_t index) {
			return items[index];
		}

		const T& operator[](const size_t index) const {
			return items[index];
		}

		Vector& operator+=(const Vector& right) {
			for (auto i = 0; i < N; i++)
				items[i] += right.items[i];
			return *this;
		}

		Vector& operator-=(const Vector& right) {
			for (auto i = 0; i < N; i++)
				items[i] -= right.items[i];
			return *this;
		}

	private:
		std::array<T, N> items;
	};

	// See https://arne-mertz.de/2015/01/operator-overloading-common-practice/
	template<typename T, size_t N>
	Vector<T, N> operator+(const Vector<T, N>& left, const Vector<T, N>& right) {
		Vector<T, N> result(left);
		result += right;
		return result;
	}

	template<typename T, size_t N>
	Vector<T, N> operator-(const Vector<T, N>& left, const Vector<T, N>& right) {
		Vector<T, N> result(left);
		result -= right;
		return result;
	}

	// SquareMatrix

	// See https://codereview.stackexchange.com/questions/186317/short-square-matrix-class-in-c-using-an-array
	template<typename T, size_t N>
	class SquareMatrix
	{
		// See https://stackoverflow.com/questions/16976720/how-do-i-restrict-a-template-class-to-certain-built-in-types
		static_assert(std::is_floating_point<T>::value, "Matrix can only be instantiated with floating point types");

	public:

		SquareMatrix() {
			for (int x = 0; x < N; x++)
				for (int y = 0; y < N; y++)
					items[y][x] = x == y ? static_cast<T>(1) : static_cast<T>(0);
		}

		const T& at(const size_t row, const size_t column) const {
			if (row >= N || column >= N) throw std::out_of_range("indices must be < " + std::to_string(N));
			return items[row][column];
		}

		Vector<T, N>& operator[](const size_t index) {
			return items[index];
		}

		const Vector<T, N>& operator[](const size_t index) const {
			return items[index];
		}

		const Vector<T, N>& row(const size_t index) const {
			if (index >= N) throw std::out_of_range("indices must be < " + std::to_string(N));
			return items[index];
		}

		Vector<T, N> column(const size_t index) const {
			if (index >= N) throw std::out_of_range("indices must be < " + std::to_string(N));

			Vector<T, N> result;
			auto i = 0;
			for (auto& row : items) {
				result[i] = row[index];
				i++;
			}

			return result;
		}

		SquareMatrix& operator+=(const SquareMatrix& right) {
			for (auto i = 0; i < N; i++)
				items[i] += right.items[i];
			return *this;
		}

		SquareMatrix& operator-=(const SquareMatrix& right) {
			for (auto i = 0; i < N; i++)
				items[i] -= right.items[i];
			return *this;
		}

		Vector<T, N - 1> extractTranslation() const {
			Vector<T, N - 1> result;
			for (size_t i = 0; i < N - 1; i++)
				result[i] = items[N - 1][i];
			return result;
		}

	private:
		std::array<Vector<T, N>, N> items;
	};

	template<typename T, size_t N>
	SquareMatrix<T, N> operator+(const SquareMatrix<T, N>& left, const SquareMatrix<T, N>& right) {
		SquareMatrix<T, N> result(left);
		result += right;
		return result;
	}

	template<typename T, size_t N>
	SquareMatrix<T, N> operator-(const Vector<T, N>& left, const SquareMatrix<T, N>& right) {
		SquareMatrix<T, N> result(left);
		result -= right;
		return result;
	}
}