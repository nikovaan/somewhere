#pragma once
#include "BaseCharacter.h"

// 7) Interface for swimming behavior
class Swimmable
{
public:
    void swim() const {
        std::cout << "Swimming!" << std::endl;
    }
};

// 1) Derived class inheriting from Character and Swimmable
class SwimmingCharacter : public Character, public Swimmable
{
public:
    SwimmingCharacter(const std::string& name) : Character(name) {}
    bool canSwim() const override
    {
        return true;
    }
};