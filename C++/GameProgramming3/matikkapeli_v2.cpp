#include <iostream>
#include <random>
#include <math.h>

int RandomGenerator(int _min, int _max)
{
    std::random_device _randomDevice;
    std::mt19937 _randomNumber(_randomDevice());
    std::uniform_int_distribution<> _valueRange(_min, _max);
    return _valueRange(_randomNumber);
}

float Calculate(int _x1, int _x2, int _operand)
{
    float _x1Float, _x2Float;
    switch (_operand)
    {
    case 0: return _x1 + _x2;
    case 1: return _x1 - _x2;
    case 2: return _x1 * _x2;
    case 3:
        _x1Float = static_cast<float>(_x1);
        _x2Float = static_cast<float>(_x2);
        return _x1Float / _x2Float;
    default: std::cout << "Invalid operation somehow.\n";
        break;
    }
    return 0;
}

void RoundPrompt(int _x1, int _x2, int _operand)
{
    std::cout << "What is " << _x1;
    switch (_operand)
    {
    case 0: std::cout << " + ";
        break;
    case 1: std::cout << " - ";
        break;
    case 2: std::cout << " * ";
        break;
    case 3: std::cout << " / ";
        break;
    default: std::cout << "\n\nSomething went wrong\n\n";
        break;
    }
    std::cout << _x2 << "?\n";
}

int CheckAnswer(float _answer, float _result)
{
    if (std::fabs(_answer - _result) < 0.001f)
    {
        std::cout << "Correct!\n";
        return 1;
    }
    else
    {
        std::cout << "Wrong!\n";
        return 0;
    }
}

void ShowResults(int _score, int _rounds)
{
    std::cout << "Game finished.\n" << "You scored " << _score << " out of a maximum of "
        << _rounds << " points!" << std::endl;
    if (_score == _rounds)
    {
        std::cout << "You got a perfect score! Incredible!\n";
    }
}

int PlayGame(int _rounds)
{
    int _score = 0;
    int _x1, _x2, _operand;
    float _answer, _result;
    for (int i = 0; i < _rounds; i++)
    {
        _x1 = RandomGenerator(1, 10);
        _x2 = RandomGenerator(1, 10);
        _operand = RandomGenerator(0, 3);
        RoundPrompt(_x1, _x2, _operand);
        std::cin >> _answer;
        _result = Calculate(_x1, _x2, _operand);
        _score = _score + CheckAnswer(_answer, _result);
    }
    return _score;
}

int main()
{
    int rounds = 0;
    int score = 0;
    std::cout << "This is a basic math quiz game.\n" << "How many rounds would you like to play?\n";
    std::cin >> rounds;
    std::cout << "Now starting " << rounds << " of basic math questions.\n\n\n";
    score = PlayGame(rounds);
    ShowResults(score, rounds);
    std::cout << "\n\nThank you for playing!" << std::endl << "I will see you next!";
}