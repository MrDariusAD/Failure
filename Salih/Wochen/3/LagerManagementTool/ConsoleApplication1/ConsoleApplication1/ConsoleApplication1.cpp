#include "pch.h"
#include <iostream>
#include <string>
#include <list>
#include <cctype>
#include "LagerObjekt.h"
#include "ConsoleApplication1.h"
using namespace std;

list<LagerObjekt*> bestand;
int hauptmenü();
void einkauf();
void verkauf();
void bestandsliste(bool);
bool is_number(const std::string&);
LagerObjekt* getListObject(int);


int main()
{
	

	bestand.push_back((new LagerObjekt("Beispiel1", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel2", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel3", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel4", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel5", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel6", 5, 12.0, LagerObjekt::typ::Verkauf)));
	bestand.push_back((new LagerObjekt("Beispiel7", 5, 12.0, LagerObjekt::typ::Verkauf)));

	while (true)
	{
		int aktion;
		do {
			aktion = hauptmenü();
		} while (aktion == 0);

		switch (aktion)
		{
		case 1:einkauf();
			break;
		case 2: verkauf();
			break;
		case 3: bestandsliste(true);
			break;
		};
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

	return 0;

}

bool is_number(const std::string& s)
{
	std::string::const_iterator it = s.begin();
	while (it != s.end() && std::isdigit(*it)) ++it;
	return !s.empty() && it == s.end();
}

void einkauf()
{
	string newName = "";
	int newAnzahl = 0;
	double newPreis = 0.0;
	int t;

	while (1)
	{
		system("CLS");
		cout << "Name: ";
		getline(std::cin >> std::ws, newName);
		if (newName == "stop" || newName == "STOP" || newName == "Stop")return;
		cout << "\nAnzahl: ";
		cin >> newAnzahl;
		cout << "\nPreis: ";
		cin >> newPreis;
		cout << "\nTyp(0=Verkauf, 1=Eigenbedarf): ";
		cin >> t;
		bestand.push_back((new LagerObjekt(newName, newAnzahl, newPreis, (LagerObjekt::typ)t)));
	}

	return;
}

void verkauf()
{
	string eingabe;
	int IEingabe;
	string anzahl;
	int IAnzahl;
	bestandsliste(false);
	cout << endl << "Welches Objekt wird verkauft? Nr. ";

	cin >> eingabe;

	if (is_number(eingabe))
	{
		IEingabe = stoi(eingabe);
	}

	cout << endl << endl << "Wie viele Objekte werden verkauft? : ";

	cin >> anzahl;

	if (is_number(anzahl))
	{
		IAnzahl = stoi(anzahl);
	}

	if (IAnzahl > getListObject(IEingabe)->anzahl)throw new exception("Anzahl der zu verkaufenden Objekte kann nicht größer als der aktuelle Bestand sein");
	
	
	
	
	getListObject(IEingabe)->anzahl = getListObject(IEingabe)->anzahl-IAnzahl;
}

void bestandsliste(bool pause)
{
	system("CLS");

	cout << "Nr." << " -- " << "Name" << " -- " << "Anzahl" << " -- " << "Preis" << " -- " << "Typ" << endl;
	cout << "--------------------------------------"<<endl;

	int i = 0;
	for (auto crs : (bestand))
	{
		cout << i << ". -- " << crs->name << " -- " << crs->anzahl << " -- " << crs->preis << " -- ";
		switch (crs->t)
		{
		case crs->typ::Verkauf: cout << "Verkauf";
			break;
		case crs->typ::Eigenbedarf: cout << "Eigenbedarf";
			break;
		default: cout << "ERROR";
			break;
		};
		cout << endl;
		i = i + 1;
	}
	if(pause)system("pause");
}

LagerObjekt* getListObject(int i)
{
	int nr = 0;
	for (auto crs : bestand)
	{
		if (nr == i)return crs;
		nr = nr + 1;
	}
	return nullptr;
}