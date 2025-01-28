#pragma once

#include <iostream>

template< typename tyyppi>
class OmaPino
{
public:
	OmaPino(int koko);
	//	~OmaPino(void) { delete[] pinoTaulu; }

	void push(tyyppi alkio);
	tyyppi pop();
	void print();

private:
	int max;
	int top;
	tyyppi* pinoTaulu;
};

// Konstruktori
template< typename tyyppi >
OmaPino< tyyppi >::OmaPino(int koko)
{
	if (koko > 0)
	{
		max = koko;
		top = -1;
		pinoTaulu = new tyyppi[koko];
	}
	else
	{
		throw std::domain_error("Stack size must be a positive integer.");
	}
}

// Alkion lisääminen pinoon
template< typename tyyppi >
void OmaPino< tyyppi >::push(tyyppi alkio)
{
	if (top != (max - 1))
	{
		pinoTaulu[++top] = alkio;
	}
	else
	{
		throw std::length_error("Stack is full.");
	}
}

// Päälimmäisen alkion palauttaminen ja poistaminen pinosta
template< typename tyyppi >
tyyppi OmaPino< tyyppi >::pop()
{
	tyyppi alkio;

	if (top != -1)
	{
		alkio = pinoTaulu[top--];
	}
	else
	{
		throw std::length_error("Stack is empty.");
	}
	return alkio;
}

template<typename tyyppi>
void OmaPino<tyyppi>::print()
{
	for (int i = 0; i < top + 1; i++)
	{
		std::cout << pinoTaulu[i] << " ";
	}

	std::cout << std::endl;
}
