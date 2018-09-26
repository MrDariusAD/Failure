#include "pch.h"
#include <iostream>
#include <string>
#include <list>
#include <cctype>
#include "LagerObjekt.h"
#include "ConsoleApplication1.h"
using namespace std;

int hauptmenü();
void einkauf(list<LagerObjekt>*);
void verkauf();
void bestandsliste();
bool is_number(const std::string&);

int main()
{
	list<LagerObjekt> bestand;

	bestand.push_back(*(new LagerObjekt("Beispiel1", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel2", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel3", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel4", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel5", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel6", 5, 12.0)));
	bestand.push_back(*(new LagerObjekt("Beispiel7", 5, 12.0)));

	int aktion;
	do{
		aktion = hauptmenü();
	} while (aktion == 0);

	switch (aktion)
	{
	case 1:einkauf(&bestand);
		break;
	case 2: verkauf();
		break;
	case 3: bestandsliste();
		break;
	}
}

int hauptmenü()
{
	system("CLS");
	string eingabe;

	cout << "Wilkommen im Lager Management Programm. Was möchten sie tun?\n"<<endl<<endl;
	
	cout << "1 - Einkauf" << endl;
	cout << "2 - Verkauf" << endl;
	cout << "3 - Bestandsausgabe" << endl;
	cout << "4 - Beenden"<< endl;

	cout << "Ihre Auswahl: ";
	
	cin >> eingabe;

	if (is_number(eingabe))
	{
		if (stoi(eingabe) <= 4 && stoi(eingabe) >= 1)
		{
			return stoi(eingabe);
		}
	}

	cout << stoi(eingabe);

	return 0;

}

bool is_number(const std::string& s)
{
	std::string::const_iterator it = s.begin();
	while (it != s.end() && std::isdigit(*it)) ++it;
	return !s.empty() && it == s.end();
}

void einkauf(list<LagerObjekt>* bstd)
{
	string newName = "";
	int newAnzahl = 0;
	double newPreis = 0.0;

	while (newName != "stop" || newName != "STOP" || newName != "Stop")
	{
		cout << "Name: ";
		getline(std::cin, newName);
		cout << "\nAnzahl: ";
		cin >> newAnzahl;
		cout << "\Preis: ";
		cin >> newPreis;
		bstd->push_back(*(new LagerObjekt(newName, newAnzahl, newPreis)));
	}

	return;
}

void verkauf()
{

}

void bestandsliste()
{

}