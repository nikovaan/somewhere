#include <iostream>

using namespace std;
class Luku
{
private:
	int x;
public:
	Luku(int p_x) { x = p_x; }
	friend Luku operator++(Luku&);
	friend Luku operator++(Luku&, int);
	void Nayta() { cout << x; }
};
Luku operator++(Luku& olio)
{
	olio.x++;
	return (olio);
}
Luku operator++(Luku& olio, int)
{
	Luku apu = olio;
	olio.x++;
	return apu;
}

int main()
{
	Luku Luku1(55);
	Luku Luku2 = Luku1++;
	cout << "Luku2 on: ";
	Luku2.Nayta();
	cout << "\nLuku1 on tässä vaiheessa: ";
	Luku1.Nayta();
	Luku Luku3 = ++Luku1;
	cout << "\nLuku3 on: ";
	Luku3.Nayta();

	int number = 3;
	int number2 = number++;
	cout << endl << number2 << endl;
	cout << number << endl;
	int number3 = ++number;
	cout << number3 << endl;
	
	return 0;
}