#include <iostream>
#include <random>
#include <math.h>

int main()
{
    std::random_device randomDevice;
    std::mt19937 randomNumber(randomDevice());
    std::uniform_int_distribution<> valueRange(0, 10);
    std::uniform_int_distribution<> operandRange(0, 3);
    int x1, x2, operand, score, rounds;
    score = 0;
    rounds = 0;

    std::cout << "This is a basic math quiz game.\n";
    while (rounds < 5)
    {
        x1 = valueRange(randomNumber);
        x2 = valueRange(randomNumber);
        operand = operandRange(randomNumber);

        if (operand == 3 && x2 == 0)
        {
            while (true)
            {
                x2 = valueRange(randomNumber);
                if (x2 != 0)
                {
                    break;
                }
            }
        }
        rounds = rounds + 1;
        if (operand == 0)
        {
            int result;
            std::cout << "What is " << x1 << " + " << x2 << "?\n";
            std::cin >> result;
            if (result == x1 + x2)
            {
                std::cout << "Congratulations! That is the correct answer!\n";
                score = score + 1;
            }
            else
            {
                std::cout << "That is the wrong answer, too bad.\n";
            }
        }
        else if (operand == 1)
        {
            int result;
            std::cout << "What is " << x1 << " - " << x2 << "?\n";
            std::cin >> result;
            if (result == x1 - x2)
            {
                std::cout << "Congratulations! That is the correct answer!\n";
                score = score + 1;
            }
            else
            {
                std::cout << "That is the wrong answer, too bad.\n";
            }
        }
        else if (operand == 2)
        {
            int result;
            std::cout << "What is " << x1 << " * " << x2 << "?\n";
            std::cin >> result;
            if (result == x1 * x2)
            {
                std::cout << "Congratulations! That is the correct answer!\n";
                score = score + 1;
            }
            else
            {
                std::cout << "That is the wrong answer, too bad.\n";
            }
        }
        else if (operand == 3)
        {
            float result;
            std::cout << "What is " << x1 << " / " << x2 << "?\n";
            std::cin >> result;
            if (result == x1 / x2)
            {
                std::cout << "Congratulations! That is the correct answer!\n";
                score = score + 1;
            }
            else
            {
                std::cout << "That is the wrong answer, too bad.\n";
            }
        }
    }

    std::cout << "You have finished the game! Your score was " << score << " points."
        << std::endl << "Thank you for playing, see you next time!";
}