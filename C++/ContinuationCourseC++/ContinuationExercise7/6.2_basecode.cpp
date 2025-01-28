// 0) Create instances of different characters
// 1) Derived class inheriting from Character and Swimmable
// 2) Derived class inheriting from Character, Flyable, and Swimmable
// 3) Derived class inheriting from Character and Flyable
// 4) Base class for the character
// 5) Display information and behaviors
// 6) Interface for flying behavior
// 7) Interface for swimming behavior

#include <iostream>
#include <string>
#include <random>

// 4) Base class for the character
class Character
{
public:
    Character(const std::string& name) : name(name) {}

    void displayInfo() const
    {
        std::cout << name;
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

// 6) Interface for flying behavior
class Flyable
{
public:
    void fly() const
    {
        std::cout << "Flying!" << std::endl;
    }
};

// 7) Interface for swimming behavior
class Swimmable
{
public:
    void swim() const
    {
        std::cout << "Swimming!" << std::endl;
    }
};

// Interface for burrowing behaviour.
class Burrowable
{
public:
    void swim() const
    {
        std::cout << "Burrowing!" << std::endl;
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

// Class derived from Character and Burrowable.
class BurrowingCharacter : public Character, public Burrowable
{
public:
    BurrowingCharacter(const std::string& name) : Character(name) {}
    bool canBurrow() const override
    {
        return true;
    }
};

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

// Class inheriting from Character, Flyable, Burrowable.
class FlyingBurrowingCharacter : public Character, public Flyable, public Burrowable
{
public:
    FlyingBurrowingCharacter(const std::string& name) : Character(name) {}
    bool canFly() const override
    {
        return true;
    }
    bool canBurrow() const override
    {
        return true;
    }
};

// Class inheriting from Character, Flyable, Burrowable.
class SwimmingBurrowingCharacter : public Character, public Flyable, public Burrowable
{
public:
    SwimmingBurrowingCharacter(const std::string& name) : Character(name) {}
    bool canSwim() const override
    {
        return true;
    }
    bool canBurrow() const override
    {
        return true;
    }
};

// This was an attempt at sidestepping the scope issue I created for myself. Didn't work.
void AdventureTime(Character _character, int _energy, int _ability)
{
    int _choice, obstacle; 
    std::cout << "Venture forth, ";
    _character.displayInfo();
    std::cout << "!" << std::endl << std::endl;
    std::cout << _character.canBurrow() << std::endl;
    std::cout << _character.canSwim() << std::endl;
    std::cout << _character.canFly() << std::endl;
    while (_energy > 0)
    {
        std::cout << "An obstacle appears before you: ";
        std::random_device randomDevice;
        std::mt19937 randomNumber(randomDevice());
        std::uniform_int_distribution<> intDistribution(1, 6);
        obstacle = intDistribution(randomNumber);
        if (obstacle == 1)
        {
            std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canFly())
            {
                _character.displayInfo();
                std::cout << " easily flies over the chasm!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canFly())
            {
                _character.displayInfo();
                std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
        else if (obstacle == 2)
        {
            std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canSwim())
            {
                _character.displayInfo();
                std::cout << " easily swims accross the river!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canSwim())
            {
                _character.displayInfo();
                std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
        else if (obstacle == 3)
        {
            std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " easily burrows through the stone!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
        else if (obstacle == 4)
        {
            std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canFly() && _character.canSwim())
            {
                _character.displayInfo();
                std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canFly() || !_character.canSwim())
            {
                _character.displayInfo();
                std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
        else if (obstacle == 5)
        {
            std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canFly() && _character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canFly() || !_character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
        else if (obstacle == 6)
        {
            std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
            std::cin >> _choice;
            if (_choice == 0 && _character.canSwim() && _character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
            }
            else if (_choice == 0 && !_character.canSwim() || !_character.canBurrow())
            {
                _character.displayInfo();
                std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                    << "After a fierce struggle, ";
                _character.displayInfo();
                std::cout << " makes it accross." << std::endl << std::endl;
                _energy = _energy - 4;
            }
            else if (_choice == 1)
            {
                _character.displayInfo();
                std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                _energy = _energy - 2;
            }
            else
            {
                std::cout << "Invalid choice, the game will now close." << std::endl;
                std::abort();
            }
        }
    }
}

int main()
{
    // 0) Create instances of different characters
    /*FlyingCharacter flyingCharacter("Superman");
    SwimmingCharacter swimmingCharacter("AquaMan");
    FlyingSwimmingCharacter flyingSwimmingCharacter("HybridHero");

    // 5) Display information and behaviors
    flyingCharacter.displayInfo();
    flyingCharacter.fly();

    swimmingCharacter.displayInfo();
    swimmingCharacter.swim();

    flyingSwimmingCharacter.displayInfo();
    flyingSwimmingCharacter.fly();
    flyingSwimmingCharacter.swim();*/

    std::string playerInput;
    int playerAbilities;
    int playerEnergy;
    bool extraEnergy = false;
    int obstacle;

    std::cout << "Enter the name for your character: ";
    std::cin >> playerInput;
    Character playerCharacter(playerInput);
    std::cout << "What abilities does your character have? 0 for random, 1 for flying," << std::endl
        << "2 for swimming, 3 for burrowing, 4 for flying and swimming," << std::endl
        << "5 for flying and burrowing, 6 for swimming and burrowing." << std::endl;
    std::cin >> playerAbilities;
    if (playerAbilities == 0)
    {
        std::random_device randomDevice;
        std::mt19937 randomNumber(randomDevice());
        std::uniform_int_distribution<> intDistribution(1, 6);
        playerAbilities = intDistribution(randomNumber);
        extraEnergy = true;
    }
    if (playerAbilities == 1)
    {
        FlyingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else if (playerAbilities == 2)
    {
        SwimmingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else if (playerAbilities == 3)
    {
        BurrowingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else if (playerAbilities == 4)
    {
        FlyingSwimmingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else if (playerAbilities == 5)
    {
        FlyingBurrowingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else if (playerAbilities == 6)
    {
        SwimmingBurrowingCharacter playerCharacter(playerInput);
        if (extraEnergy == true)
        {
            playerEnergy = 10;
        }
        else
        {
            playerEnergy = 5;
        }
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }
    }
    else
    {
        std::cout << "No valid ability choice, creating a base character." << std::endl;
        Character playerCharacter(playerInput);
        playerEnergy = 15;
        std::cout << "Venture forth, ";
        playerCharacter.displayInfo();
        std::cout << "!" << std::endl;
        while (playerEnergy > 0)
        {
            std::cout << "An obstacle appears before you: ";
            std::random_device randomDevice;
            std::mt19937 randomNumber(randomDevice());
            std::uniform_int_distribution<> intDistribution(1, 6);
            obstacle = intDistribution(randomNumber);
            if (obstacle == 1)
            {
                std::cout << "a deep chasm! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the chasm!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly())
                {
                    playerCharacter.displayInfo();
                    std::cout << " does a mighty leap over the chasm, but it is not enough!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 2)
            {
                std::cout << "a fierce river! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims accross the river!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " makes an attempt to swim, but gets battered by the raging currents!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 3)
            {
                std::cout << "a tall mountain! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily burrows through the stone!" << std::endl << std::endl;
                }
                else if (playerAbilities == 0 && !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb the mountain, even though it looks perilous!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 4)
            {
                std::cout << "a great waterfall! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims up the waterfall, almost like taking flight!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canSwim())
                {
                    playerCharacter.displayInfo();
                    std::cout << " tries to climb up, but finding any footholds seems impossible!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 5)
            {
                std::cout << "a great mountain surrounded by ravines! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canFly() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily flies over the ravine and straight through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canFly() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " spends a lot of time setting up ropes to go accross the ravine, only to have a mountain to climb still!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
            else if (obstacle == 6)
            {
                std::cout << "a pictoresque mountain surrounded by a pristine lake! Press 0 to go through, or 1 to look for another path." << std::endl;
                std::cin >> playerAbilities;
                if (playerAbilities == 0 && playerCharacter.canSwim() && playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " easily swims over the lake and through the mountain!" << std::endl << std::endl;
                }
                else if (playerAbilities == 1)
                {
                    playerCharacter.displayInfo();
                    std::cout << " deems it too risky to try and get accross directly, and opts to look for another path." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 2;
                }
                else if (playerAbilities == 0 && !playerCharacter.canSwim() || !playerCharacter.canBurrow())
                {
                    playerCharacter.displayInfo();
                    std::cout << " builds a makeshift raft, only to struggle with the mountain!" << std::endl
                        << "After a fierce struggle, ";
                    playerCharacter.displayInfo();
                    std::cout << " makes it accross." << std::endl << std::endl;
                    playerEnergy = playerEnergy - 4;
                }
                else
                {
                    std::cout << "Invalid choice, the game will now close." << std::endl;
                    return 0;
                }
            }
        }

    }

    playerCharacter.displayInfo();
    std::cout << " is now too tired to continue and retreats to adventure another day." << std::endl
        << "Thank you for playing!" << std::endl;

    return 0;
}