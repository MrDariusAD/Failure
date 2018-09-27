#pragma once
#include <string>

class LagerObjekt
{
public:
	enum typ { Verkauf, Eigenbedarf };

	LagerObjekt(std::string newName, int newAnzahl, double newPreis, typ newTyp);
	~LagerObjekt();
	
	std::string name;
	int anzahl;
	double preis;
	typ t;
};

