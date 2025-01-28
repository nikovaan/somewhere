#include <iostream>
using namespace std;
class Luokka;
class Ystava1
{
public:	void Ali1(const Luokka& olio);
	  void Ali2(const Luokka* olio);
};
class Ystava2
{
public:	void Ali2(const Luokka* olio);
};
void Globaali(const Luokka& olio);
class Luokka
{
private:
	int x;
	friend void Globaali(const Luokka&);
	friend class Ystava1;
	friend void Ystava2::Ali2(const Luokka*);
public:
	Luokka(int p_x) { x = p_x; }
};

void Ystava1::Ali1(const Luokka& olio)
{
	cout << olio.x;
}
void Ystava1::Ali2(const Luokka* olio)
{
	cout << olio->x;
}
void Ystava2::Ali2(const Luokka* olio)
{
	cout << olio->x;
}
void Globaali(const Luokka& olio)
{
	cout << olio.x;
}

int main()
{
	Luokka olio(1111111111);
	Ystava1 ystava1;
	Ystava2 ystava2;
	Globaali(olio);
	ystava1.Ali1(olio);
	ystava1.Ali2(&olio);
	ystava2.Ali2(&olio);
	return 0;
}