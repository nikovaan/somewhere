#pragma once

template <typename SuitType>
class Suit
{
private:
    std::vector<Card<SuitType>> cards;

public:
    void AddCard(SuitType value)
    {
        Card<SuitType> card(value);
        cards.push_back(card);
    }

    void Shuffle()
    {
        std::random_device randomDevice;
        std::mt19937 randomNumber(randomDevice());
        std::shuffle(cards.begin(), cards.end(), randomNumber);
    }

    void Display()
    {
        for (const auto& card : cards)
        {
            std::cout << card.Value << " ";
        }
        std::cout << std::endl;
    }
};