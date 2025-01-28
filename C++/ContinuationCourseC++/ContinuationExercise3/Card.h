#pragma once

template <typename CardType>
class Card
{
public:
    CardType Value;

    Card(CardType value) : Value(value) {}
};