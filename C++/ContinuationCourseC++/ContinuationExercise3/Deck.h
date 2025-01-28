#pragma once
#include <typeinfo>

template <typename DeckType>
class Deck
{
private:
    std::vector<Card<DeckType>> cards;
    std::vector<Card<DeckType>> hand;
    std::vector<Card<DeckType>> discards;
    std::vector<Card<DeckType>> deck;
    //DeckType maxValue, minValue, medianValue, guessAttempt;

    // Puts all cards from hand and discard pile back into the deck. Mostly used when shuffling the deck.
    void resetDeck()
    {
        if (!discards.empty)
        {
            for (int i : discards)
            {
                deck.push_back(discards.back);
                discards.pop_back;
            }
        }
        if (!hand.empty)
        {
            for (int i : hand)
            {
                deck.push_back(hand.back);
                hand.pop_back;
            }
        }
    }

public:
    // Add a card to the pack
    void AddCard(DeckType value)
    {
        Card<DeckType> card(value);
        cards.push_back(card);
    }

    // Checks that all cards are in the pack and then shuffles the pack.
    void Shuffle()
    {
        /*if (!hand.empty || !discards.empty)
        {
            resetDeck();
        }*/
        std::random_device randomDevice;
        std::mt19937 randomNumber(randomDevice());
        std::shuffle(cards.begin(), cards.end(), randomNumber);
    }

    // Display the cards in the pack
    void Display()
    {
        for (const auto& card : cards)
        {
            std::cout << card.Value << " ";
        }
        std::cout << std::endl;
    }

    bool HighOrLow(std::string _guess)
    {
        if (_guess == "high" || _guess == "High")
        {
            std::cout << cards.back().Value << " is higher than " << cards.end()[-2].Value << "?" << std::endl;
            return cards.back().Value > cards.end()[-2].Value;
        }
        else if (_guess == "low" || _guess == "Low")
        {
            std::cout << cards.back().Value << " is lower than " << cards.end()[-2].Value << "?" << std::endl;
            return cards.back().Value < cards.end()[-2].Value;
        }
        else
        {
            std::cout << "Incorrect guess entry, this is counted as a loss." << std::endl;
            return false;
        }
        /*if (_guess == "high" || _guess == "High")
        {
            guessAttempt = cards.back();
            cards.pop_back();
            return guessAttempt > medianValue;
        }
        else if (_guess == "low" || _guess == "Low")
        {
            guessAttempt = cards.back();
            cards.pop_back();
            return guessAttempt < medianValue;
        }
        else
        {
            std::cout << "Incorrect guess entry, this is counted as a loss." << std::endl;
            return false;
        }*/
        return false;
    }

    // Draws a card from the top of the deck to hand.
    int Draw()
    {
        if (deck.empty)
        {
            std::cout << "Can't draw from empty deck." << std::endl;
            return 1;
        }
        else
        {
            hand.push_back(deck.back);
            deck.pop_back;
            return 0;
        }
    }

    // Discards a card from hand to the discards pile.
    int Discard(int _handCard)
    {
        if (hand.empty)
        {
            std::cout << "Can't discard cards from an empty hand." << std::endl;
            return 2;
        }
        else if (hand.size <= _handCard)
        {
            discards.push_back(hand[_handCard - 1]);
            hand.erase(_handCard - 1);
            return 0;
        }
        else
        {
            std::cout << "Please specify a number within range of 1 to " << hand.size << "." << std::endl;
            return 1;
        }
    }

    // Checks cards in current hand, if there are any.
    void CheckHand()
    {
        if (hand.empty)
        {
            std::cout << "You have no cards in your hand." << std::endl;
        }
        else
        {
            for (const auto& card : hand)
            {
                std::cout << card.Value << " ";
            }
            std::cout << std::endl;
        }
    }

    // Checks current discard pile, if there are any discarded cards.
    void CheckDiscards()
    {
        if (discards.empty)
        {
            std::cout << "Discards pile is empty." << std::endl;
        }
        else
        {
            for (const auto& card : discards)
            {
                std::cout << card.Value << " ";
            }
            std::cout << std::endl;
        }
    }

    // Checks cards currently in the deck, if there are any.
    void CheckDeck()
    {
        if (deck.empty)
        {
            std::cout << "Deck pile is empty." << std::endl;
        }
        else
        {
            for (const auto& card : deck)
            {
                std::cout << card.Value << " ";
            }
            std::cout << std::endl;
        }
    }
};