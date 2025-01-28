//#define NDEBUG
#include <iostream>
#include <fstream>
#include <string>
#include <limits> // for std::numeric_limits
#include <iostream>
#include <assert.h>
//#include <exception>
using namespace std;


//
/*
//ways to interrupt the execution
int main()
{
    std::cout << "Program execution starting... ";
    // 1. abort | default "abort the program" thing
    //abort();
    // 2. terminate | by default calls abort(), but can be defined to call other things too
    //terminate();
    // 3. exit
    exit(3333);
    std::cout << "... program execution ending. ";
    return 0;
}
//*/


//assert
/*
int main()
{
//ways to interrupt the execution

    int a;
    std::cout << "Give positive integer: ";
    std::cin >> a;
    a / 0;
    assert(a > 0);
    std::cout << "Correct";
    return 0;
}
//*/


//a simple file handling example to catch an error by using throw keyword
/*
int main() {
    try {
        ifstream MyReadFile("testfile3.txt");
        if (!MyReadFile)
            throw 1;
        MyReadFile.close();
    }

    catch(int x) {

        cout << "Something happened. File doesn't exist.\n";
        cout << x;
    }

}
//*/


// 
/*
// the following is not catching anything
int main()
{
    try {
        int var_x;
        cout << "Fill in a number.";
        cin >> var_x;
    }
    catch (int err_number) {

        cout << "Fill in a number, not a letter.";

    }

}
//*/

/*
int main() {
    try {
        int var_x;
        std::cout << "Fill in a number: ";
        std::cin >> var_x;

        if (std::cin.fail()) {
           std::cin.clear(); // clear the error flag
           std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); // discard invalid input
           throw 1; // throw an exception to be caught
           //throw "problem"; // text version
        }

    }

    catch (const char * err_msg) {
        std::cout << err_msg << std::endl;
    }
    catch (int err_number) {
        std::cout << "Fill in a number, not a letter." << std::endl;
    }
    catch (...) {
        std::cout << "Problem occurred." << std::endl;
    }

    return 0;
}
/*/

/*
//Division by zero example
//NB std::runtime_error
#include <iostream>
#include <stdexcept>

int main() {
    int number1 = 3000;
    int number2 = 0;

    try {
        if (number2 == 0) {
            throw std::runtime_error("Division by zero"),1;
        }
        std::cout << number1 / number2 << std::endl;
    }
    catch (const std::range_error& e) {
        std::cout << "Exception range_error caught: " << e.what() << std::endl;
    }
    catch (const std::runtime_error& e) {
        std::cout << "Exception runtime caught: " << e.what() << std::endl;
    }
    catch (const std::exception& e) {
        std::cout << "Exception caught: " << e.what() << std::endl;
    }

    return 0;
}
//*/

//OWN EXCEPTION CLASS (START) 
/*
// Define a new exception class that inherits from
// std::exception
class MyException : public exception {
private:
    std::string message;

public:
    // Constructor accepts a const char* that is used to set
    // the exception message
    MyException(const char* msg)
        : message(msg)
    {
    }

    // Override the what() method to return our message
    const char* what() const throw()
    {
        return message.c_str();
    }
};

// Usage
int main()
{
    try {
        // Throw our custom exception
        throw MyException("This is a custom exception");
    }
    catch (MyException& e) {
        // Catch and handle our custom exception
        std::cout << "Caught an exception: " << e.what() << std::endl;
    }

    return 0;
}
//*/
//OWN EXCEPTION CLASS (END) 