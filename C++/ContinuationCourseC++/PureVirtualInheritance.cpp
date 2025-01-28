#include <iostream>

class Figure
{
protected:
	double x, y;
public:
	void SetDimensions(double i, double j) { x = i; y = j; }
	virtual void PrintArea() = 0; // Pure virtual function
};

class Triangle : public Figure
{
public:
	void PrintArea()
	{
		std::cout << "This is a triangle with the following dimensions:" << std::endl;
		std::cout << "Height: " << x << std::endl;
		std::cout << "Width: " << y << std::endl;
		std::cout << "Area: " << x * y * 0.5 << std::endl;
	}
};

class Rectangle : public Figure
{
public:
	void PrintArea()
	{
		std::cout << "This is a rectangle with the following dimensions:" << std::endl;
		std::cout << "Height: " << x << std::endl;
		std::cout << "Width: " << y << std::endl;
		std::cout << "Area: " << x * y << std::endl;
	}
};

class Circle : public Figure
{
public:
	void PrintArea()
	{
		std::cout << "You must define something for a pure virtual function." << std::endl;
	}
};

/*int main()
{
	Figure* p;
	Triangle _triangle;
	Rectangle _rectangle;
	Circle _circle;

	p = &_triangle;
	p->SetDimensions(10.0, 5.0);
	p->PrintArea();

	p = &_rectangle;
	p->SetDimensions(10.0, 5.0);
	p->PrintArea();

	p = &_circle;
	p->SetDimensions(10.0, 5.0);
	p->PrintArea();
}*/