#include <iostream>
#include <algorithm>
#include <array>

int main()
{
    int count_even;
    std::array<int, 5> numbers = { 2, 6, 10, 13, 16 };
    std::cout << "Numbers are: ";
    for (int i = 0; i < 5; i++)
    {
        std::cout << numbers[i] << " ";
    }
    std::cout << "\n";

    // all_of exercise
    if (std::all_of(numbers.begin(), numbers.end(), [](int i) { return i % 2 == 0; }))
    {
        std::cout << "All numbers are even.\n";
    }
    else
    {
        std::cout << "Not all numbers are even.\n";
    }

    // count_even exercise
    count_even = std::count_if(numbers.begin(), numbers.end(), [](int i) { return i % 2 == 0; });
    std::cout << count_even << " numbers were even.\n";

    // replace_if exercise
    std::replace_if(numbers.begin(), numbers.end(), [](int i) { return i % 2 == 1; }, 14);

    std::cout << "Now numbers are: ";
    for (int i = 0; i < 5; i++)
    {
        std::cout << numbers[i] << " ";
    }
    std::cout << "\n";
}