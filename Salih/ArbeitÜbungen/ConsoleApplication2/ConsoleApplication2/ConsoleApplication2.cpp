// ConsoleApplication2.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include "pch.h"
#include <string>
#include <iostream>

using namespace std;


class Ski {

	public:
		string hersteller;
		string Modell;
		float  Laenge;
		char   Typ;
		int    anzAusgelieheneTage;
		bool   istDefekt;

		Ski() {
			hersteller = "Monster";
			Modell ="Monster Ski 2000";
			Laenge = 1.81F;
			Typ = 'A';
			anzAusgelieheneTage = 18;
			istDefekt = false;
		}

		Ski(string _hersteller, string _modell, float _laenge, char _typ)
		{
			hersteller = _hersteller;
			Modell = _modell;
			Laenge = _laenge;
			Typ = _typ;
			anzAusgelieheneTage = 0;
			istDefekt = false;
		}

};

int main()
{
	Ski("Monster", "Monster Ski 2000", 1.81F, 'A');
	Ski();

    std::cout << "Hello World!\n"; 
}

