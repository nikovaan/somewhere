#pragma once
#include <iostream>

// 4) Base class for the character
class Character
{
public:
    Character(const std::string& name) : name(name) {}

    void displayInfo() const {
        std::cout << "Character: " << name << std::endl;
    }
    virtual bool canFly() const
    {
        return false;
    }
    virtual bool canSwim() const
    {
        return false;
    }
    virtual bool canBurrow() const
    {
        return false;
    }

private:
    std::string name;
};