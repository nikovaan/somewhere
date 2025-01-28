#pragma once

#include <iostream>

//LIFO stack but self-made
template <typename tyyppi>
class OmaPino
{
public:
	OmaPino(int koko);
	void push(tyyppi alkio);
	tyyppi pop();
	void print();

private:
	int max;
	int top;
	tyyppi* pinoTaulu;
};

//constructor
template <typename tyyppi>
OmaPino<tyyppi>::OmaPino(int koko)
{
	max = koko;
	top = -1;
	pinoTaulu = new tyyppi[koko];
}

//pushing an entry into the stack
template <typename tyyppi>
void OmaPino<tyyppi>::push(tyyppi alkio)
{
	if (top != (max - 1))
	{
		pinoTaulu[++top] = alkio;
	}
}

//popping the topmost entry from the stack
template <typename tyyppi>
tyyppi OmaPino<tyyppi>::pop()
{
	tyyppi alkio;
	if (top != -1)
	{
		alkio = pinoTaulu[top--];
	}
	return alkio;
}

//printing the stack
template <typename tyyppi>
void OmaPino<tyyppi>::print()
{
	for (int i = 0; i < (top + 1); i++)
	{
		std::cout << pinoTaulu[i] << " ";
	}
	std::cout << std::endl;
}