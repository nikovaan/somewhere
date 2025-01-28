#include <iostream>
#include "namespaceTrial.h"
#include "namespaceTrial2.h"

// Turhat koska käytössä on oma::x ja oma2::x
//using namespace oma;
//using namespace oma2;

int main()
{
	std::cout << oma::x << std::endl;
	std::cout << oma2::x << std::endl;
}