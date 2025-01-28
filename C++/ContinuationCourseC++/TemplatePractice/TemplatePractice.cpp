#include <iostream>
#include <string>
#include "OmaPino.h"

int main()
{
    OmaPino<std::string> omaPino(5);

    omaPino.push("bdsaf");
    omaPino.push("jytr");
    omaPino.push("77aa");
    omaPino.push("lkuy");
    omaPino.push("alkupalana alkio");

    std::cout << "Pinossa on: ";
    omaPino.print();

    std::cout << "Poppaus: " << omaPino.pop() << std::endl;
    std::cout << "Poppaus: " << omaPino.pop() << std::endl;
    std::cout << "Pinossa on: ";
    omaPino.print();
}
