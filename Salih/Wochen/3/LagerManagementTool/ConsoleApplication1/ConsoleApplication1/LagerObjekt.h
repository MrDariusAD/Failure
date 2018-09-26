#pragma once
#include <string>

class LagerObjekt
{
public:
	LagerObjekt(std::string newName, int newAnzahl, double newPreis);
	~LagerObjekt();
	
	std::string name;
	int anzahl;
	double preis;


};

