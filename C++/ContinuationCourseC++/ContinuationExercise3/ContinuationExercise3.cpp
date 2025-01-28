#include <iostream>
#include <vector>
#include <algorithm>
#include <random>
#include "Card.h"
#include "Deck.h"
#include <string>
#include <typeinfo>

// I had 3 different ideas for this and the first two crashed and burned. This is the third and mostly successful attempt.
// The code for the failed attempts are left here just for posterity, and also maybe I'll revisit this if I remember when I understand more.
int main() {
    std::string playerInput;
    bool guessAttempt;
    int currentScore = 0;

    std::cout << "This is a game of high or low." << std::endl
        << "Create a deck of char or short type and then guess either high or low." << std::endl
        << "It compares the topmost card on the shuffled deck to the next one."
        << std::endl << "Good luck!" << std::endl;
    std::cout << "Create a type of deck: ";
    std::cin >> playerInput;
    if (playerInput == "char")
    {
        Deck<char> playerDeck;
        for (short i = 0; i < 256; i++)
        {
            playerDeck.AddCard(i);
        }
        playerDeck.Shuffle();
        while (true)
        {
            std::cout << "High or low: " << std::endl;
            std::cin >> playerInput;
            guessAttempt = playerDeck.HighOrLow(playerInput);
            if (guessAttempt == true)
            {
                currentScore = currentScore + 1;
                std::cout << "Success! You now have " << currentScore << " points!" << std::endl;
            }
            else
            {
                std::cout << "Failure! You ended at " << currentScore << " points. Better luck next time!" << std::endl
                    << "The program will now close." << std::endl;
                return 0;
            }
            playerDeck.Shuffle();
        }
    }
    else if (playerInput == "short")
    {
        Deck<short> playerDeck;
        for (int i = 0; i < 65536; i++)
        {
            playerDeck.AddCard(i);
        }
        playerDeck.Shuffle();
        while (true)
        {
            std::cout << "High or low: " << std::endl;
            std::cin >> playerInput;
            guessAttempt = playerDeck.HighOrLow(playerInput);
            if (guessAttempt == true)
            {
                currentScore = currentScore + 1;
                std::cout << "Success! You now have " << currentScore << " points!" << std::endl;
            }
            else
            {
                std::cout << "Failure! You ended at " << currentScore << " points. Better luck next time!" << std::endl
                    << "The program will now close." << std::endl;
                return 0;
            }
            playerDeck.Shuffle();
        }
    }
    // These two are a really bad idea actually so let's not. There's probably some smart way to do this but I didn't find it at least.
    /*else if (playerInput == "int")
    {
        Deck<int> playerDeck;
        for (short i = 0; i < 4294967296; i++)
        {
            playerDeck.AddCard(i);
        }
        playerDeck.Shuffle();
    }
    else if (playerInput == "long")
    {
        Deck<long> playerDeck;
        for (short i = 0; i < 256; i++)
        {
            playerDeck.AddCard(i);
        }
        playerDeck.Shuffle();
    }*/
    else
    {
        std::cout << "Incorrect input, the program will now exit." << std::endl;
        return 0;
    }

    /*while (!playerDeck.empty)
    {

    }*/
    
    //for (long long i = std::numeric_limits<*playerDeck>::min(); i < std::numeric_limits<*playerDeck>::max(); i++)
    //{

    //}

}