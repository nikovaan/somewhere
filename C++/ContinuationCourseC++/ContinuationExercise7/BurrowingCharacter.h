#pragma once
#include "BaseCharacter.h"

// 6) Interface for flying behavior
class Burrowable
{
public:
    void fly() const {
        std::cout << "Burrowing!" << std::endl;
    }
};

// 3) Derived class inheriting from Character and Flyable
class BurrowingCharacter : public Character, public Burrowable
{
public:
    BurrowingCharacter(const std::string& name) : Character(name) {}
    bool canBurrow() const override
    {
        return true;
    }
};