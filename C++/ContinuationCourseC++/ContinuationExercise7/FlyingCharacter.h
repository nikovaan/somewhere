#pragma once
#include "BaseCharacter.h"

// 6) Interface for flying behavior
class Flyable
{
public:
    void fly() const {
        std::cout << "Flying!" << std::endl;
    }
};

// 3) Derived class inheriting from Character and Flyable
class FlyingCharacter : public Character, public Flyable
{
public:
    FlyingCharacter(const std::string& name) : Character(name) {}
    bool canFly() const override
    {
        return true;
    }
};