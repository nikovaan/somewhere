#include <iostream>

class BaseClass
{
public:
    int x;
    BaseClass()
    {
        x = 1;
        y = 2;
        z = 3;
    }
    void PrintElement()
    {
        std::cout << "x: " << x << std::endl << "y: " << y << std::endl
            << "z: " << z << std::endl;
    }
protected:
    int y;
private:
    int z;
};

class DerivedClass1 : public BaseClass
{
public:
    void PrintDerived()
    {
        std::cout << "Derived x: " << x << std::endl;
        std::cout << "Derived y: " << y << std::endl;
        //std::cout << "Derived z: " << z << std::endl;
    }
};

void PrintingFunction(BaseClass obj)
{
    std::cout << "PF x: " << obj.x << std::endl;
    //std::cout << "PF x: " << obj.y << std::endl;
    //std::cout << "PF x: " << obj.z << std::endl;
    obj.PrintElement();
}

int main()
{
    BaseClass object1;
    DerivedClass1 object2;
    object1.PrintElement();
    PrintingFunction(object2);
    object2.PrintDerived();
}
