#pragma once
#include "FlyingCharacter.h"
#include "SwimmingCharacter.h"

// 2) Derived class inheriting from Character, Flyable, and Swimmable
class FlyingSwimmingCharacter : public Character, public Flyable, public Swimmable
{
public:
    FlyingSwimmingCharacter(const std::string& name) : Character(name) {}
    bool canFly() const override
    {
        return true;
    }
    bool canSwim() const override
    {
        return true;
    }
};