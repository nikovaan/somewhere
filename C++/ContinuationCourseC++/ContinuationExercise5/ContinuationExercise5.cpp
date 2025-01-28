#include <iostream>
#include <string>
#include <exception>
#include "OmaPino.h"
#include "ErrorLogging.h"

enum ValueType { PositiveInt, Integer };

void logger(LogLevel _logLevel, std::string _logMessage)
{
	ErrorLogger Logger("logfile.txt");
	Logger.log(_logLevel, _logMessage);
}

int InputValidationInt(ValueType _type)
{
	int _input;
	std::cin >> _input;
	switch (_type)
	{
	case PositiveInt:
		while (true)
		{
			if (std::cin.fail() || _input < 1)
			{
				std::cin.clear();
				std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
				std::cout << "Please enter a positive integer only: ";
				std::cin >> _input;
			}
			else
			{
				break;
			}
		}
		break;
	case Integer:
		while (true)
		{
			if (std::cin.fail())
			{
				std::cin.clear();
				std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
				std::cout << "Please enter an integer only: ";
				std::cin >> _input;
			}
			else
			{
				break;
			}
		}
		break;
	default:
		break;
	}
	return _input;
}

int main()
{
	logger(INFO, "Program started.");
	int userInput, stackSize;

	std::cout << "How big of a stack would you like to create?" << std::endl;
	stackSize = InputValidationInt(PositiveInt);

	OmaPino<int> intPino(stackSize); // I wanted to call this inside a try catch block but couldn't figure out how

	for (int i = 0; i < stackSize; i++)
	{
		std::cout << "Push an integer to stack slot " << i + 1 << ": ";
		userInput = InputValidationInt(Integer);
		try
		{
			intPino.push(userInput);
		}
		catch (const std::exception& exception)
		{
			std::cout << "Error: " << exception.what() << std::endl << "Application will now quit." << std::endl;
			logger(ERROR, exception.what());
			return 1;
		}
	}

	while (true)
	{
		std::cout << "Press 0 to quit, 1 to print stack, 2 to pop from stack, 3 to push to stack." << std::endl;
		userInput = InputValidationInt(Integer);
		switch (userInput)
		{
		case 0: std::cout << "quitting";
			logger(INFO, "Program closed normally.");
			return 0;
			break;
		case 1: intPino.print();
			break;
		case 2: 
			try
			{
				intPino.pop();
			}
			catch (const std::exception& exception)
			{
				std::cout << "Error: " << exception.what() << std::endl << "Application will now quit." << std::endl;
				logger(ERROR, exception.what());
				return 1;
			}
			break;
		case 3:
			try
			{
				std::cout << "Push an integer to the top of the stack: ";
				userInput = InputValidationInt(Integer);
				intPino.push(userInput);
				break;
			}
			catch (const std::exception& exception)
			{
				std::cout << "Error: " << exception.what() << std::endl << "Application will now quit." << std::endl;
				logger(ERROR, exception.what());
				return 1;
			}
			break;
		default: std::cout << "Incorrect input." << std::endl;
			std::cin.clear();
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
			break;
		}
	}
}
