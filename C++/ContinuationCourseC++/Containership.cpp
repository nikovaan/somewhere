#include <iostream>
#include <string>

// Engine class
class Engine {
public:
    Engine(const std::string& type, int horsepower)
        : type(type), horsepower(horsepower) {
    }

    void start() {
        std::cout << "Engine of type " << type
            << " with " << horsepower << " HP started." << std::endl;
    }

private:
    std::string type;
    int horsepower;
};

// Car class containing an Engine
class Car {
public:
    Car(const std::string& model, const std::string& engineType, int horsepower)
        : model(model), engine(engineType, horsepower) {
    }

    void drive() {
        std::cout << "Driving the " << model << std::endl;
        engine.start();  // Start the engine when driving
    }

private:
    std::string model;
    Engine engine;  // Containership: Car has an Engine
};

/*int main() {
    Car myCar("Toyota Camry", "V6", 301);
    myCar.drive();

    return 0;
}*/