#include <iostream>
#include <vector>
#include <algorithm>
#include <ctime>
#include <cstdlib>
#include <random>
#include <array>

int main()
{
	std::mt19937 mt{ std::random_device{}()};
	std::uniform_int_distribution<> RNGDistribution(1, 10);
	int RandomNumber;
	bool Win = false;
	char Confirm;

	std::string PlayerName;
	int PlayerInput;

	int HighScores[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	int CurrentScore = 0;

	std::cout << "Enter your name: ";
	std::cin >> PlayerName;
	std::cout << "Welcome, " << PlayerName << ". This is a game of odd or even. Type 1 to guess for odd, or type 2 to guess for even." << std::endl << "Alternatively type 0 to quit." << std::endl << "Good luck!" << std::endl;
	
	while (true)
	{
		RandomNumber = RNGDistribution(mt);
		std::cout << "Odd or even?" << std::endl;
		while (!(std::cin >> PlayerInput) || PlayerInput <= -1 || PlayerInput > 3)
		{
			std::cout << "Invalid input. Please enter 1 to guess odd, 2 to guess even, or 0 to quit: ";
			std::cin.clear();
			std::cin.ignore(10000, '\n');
		}

		if (PlayerInput == 0)
		{
			break;
		}
		else if (PlayerInput == 1)
		{
			if (RandomNumber % 2 == 0)
			{
				Win = false;
			}
			else
			{
				Win = true;
			}
		}
		else if (PlayerInput == 2)
		{
			if (RandomNumber % 2 == 0)
			{
				Win = true;
			}
			else
			{
				Win = false;
			}
		}
		else
		{
			std::cout << "Something went wrong.";
			return 1;
		}

		std::cout << "You guessed " << PlayerInput << std::endl << "The number was " << RandomNumber << std::endl;
		
		if (Win == true)
		{
			CurrentScore = CurrentScore + 1 + CurrentScore;
			std::cout << "Congratulations! Your score is now " << CurrentScore << std::endl;
		}
		else
		{
			if (CurrentScore > 0)
			{
				//std::find_if(&HighScores[0], &HighScores[9], [=](int i) { return (CurrentScore > i); }); find_if refuses to do what I want so for loop it is
				for (int i = 0; i < 10; i++)
				{
					if (HighScores[i] < CurrentScore)
					{
						HighScores[i] = CurrentScore;
						break;
					}
				}
				std::cout << "Congratulations! You got a new high score!" << std::endl;
			}
			else
			{
				std::cout << "Too bad! You had no success this time." << std::endl;
			}

			CurrentScore = 0;
			std::cout << "Current top 10 scores:";
			for (int i = 0; i < 10; i++)
			{
				std::cout << " " << HighScores[i];
			}
			std::cout << "." << std::endl << "Try again? (y/n) ";
			while (true)
			{
				std::cin >> Confirm;
				if (Confirm == 'Y' || Confirm == 'y')
				{
					std::cout << "Good luck!" << std::endl;
					break;
				}
				else if (Confirm == 'N' || Confirm == 'n')
				{
					std::cout << "Cheers";
					return 0;
				}
				else
				{
					std::cout << "Please enter y/n ";
				}
			}
		}
	}

	return 0;
}
