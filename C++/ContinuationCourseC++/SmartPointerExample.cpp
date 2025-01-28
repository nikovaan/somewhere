//käydään tämä koodi tunnilla läpi
#include <iostream>
#include <memory>
using namespace std;

//seuraava luokka vain demonstrointi tarkoituksessa, SmartPointterit eivät tätä vaadi
class MyClass {
public:
	MyClass() {
		cout << "Constructor invoked" << endl;
	}
	~MyClass() {
		cout << "Destructor invoked" << endl;
	}
};


int main()
//SmartPointterit allokoivat, varaavat ja vapauttavat, muistia automaattisesti
//kolmenlaisia: unique-, shared- ja weak-pointtereita
// unique pointer: osoittimella vain yksi omistaja
// shared pointer: jaettu omistajuus, mutta vain tarvittavan ajan ohjelman suorituksessa
// weak pointer: ottaa omistajuuden vain käytettäessä
//määritelty <memory>-kirjastossa: https://en.cppreference.com/w/cpp/header/memory

{
	//*	case1 uniq_ptr, basic use
	//luodaan unique pointter, integer-tyyppinen tässä tapauksessa
	unique_ptr<int>uniqPtr1 = make_unique<int>(15);
	//pointterin osoite
	cout << uniqPtr1 << endl;
	//pointterin osoitteen osoittama arvo
	cout << *uniqPtr1 << endl;

	//luodaan toinen unique pointter
	//jos yrittää suoraan asettaa ptr2:n osoittamaan ptr1:een, tulee error
	//erroria tästä:unique_ptr<int>uniqPtr2 = uniqPtr1;
	//sen sijaan voidaan siirtää (move) ptr1:sen osoittaman osoitteen omistajuus ptr2:lle
	unique_ptr<int>uniqPtr2 = move(uniqPtr1);

	//pointterin osoite
	cout << uniqPtr2 << endl;
	//pointterin osoitteen osoittama arvo
	cout << *uniqPtr2 << endl;
	//samalla ptr1:stä tulee ns. null-arvoinen ja siihen ei voida enää viitata
	//null-pointer tästä:cout << uniqPtr1 << endl;

	//tässä kohtaa otetaan käyttöön MyClass, määritelty yllä, 
	//jotta voidaan demonstroida muistiallokointia, constructoria ja destruktoria
	//normaalisti tämä tapahtuu, kun kyseisen koodilohkon {} suoritus alkaa/päättyy
	//tällä ehkäistään muistivuotoa
	//*/	case2 uniq_ptr, behave when class constructor and destructor used
	{
		unique_ptr<MyClass>uniqPtr3 = make_unique<MyClass>();
		cout << "************* ";
	}

	//*/case3 shared_ptr
	//luodaan shared pointer aivan kuten unique pointer
	shared_ptr<int>sharedPtr1 = make_shared<int>(11);
	//pointterin osoite
	cout << sharedPtr1 << endl;
	//pointterin osoitteen osoittama arvo
	cout << *sharedPtr1 << endl;

	//demonstrointi	ja use_count-metodin käyttö
	//omistajien määrä muuttuu koodilohkon {} paikkaa muutettaessa
//{	
	shared_ptr<MyClass>sharedPtr2 = make_shared<MyClass>();
	cout << "Shared count: " << sharedPtr2.use_count() << endl;
	{
		shared_ptr<MyClass>sharedPtr3 = sharedPtr2;
		cout << "Shared count: " << sharedPtr2.use_count() << endl;
	}
	cout << "Shared count: " << sharedPtr2.use_count() << endl;
	//}

		//case 4, weak_ptr
		//luodaan weak-pointer sekä shared pointer, jonka viittaus annetaan weak-pointterille
		//tutkitaan weak-pointterin käyttäytymistä debugin avulla
	weak_ptr<int> weakPtr1;
	{
		shared_ptr<int>sharedPtr3 = make_shared<int>(33);
		weakPtr1 = sharedPtr3;
		cout << "Shared count: " << sharedPtr1.use_count() << endl;
	}

}