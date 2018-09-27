#include "pch.h"
#include "LagerObjekt.h"


//LagerObjekt::LagerObjekt()
//{
//	name = "Undefined";
//	anzahl = 0;
//	preis = 0.0;
//}


LagerObjekt::LagerObjekt(std::string newName, int newAnzahl, double newPreis, LagerObjekt::typ newTyp)
{
	name = newName;
	anzahl = newAnzahl;
	preis = newPreis;
	t = newTyp;
}

LagerObjekt::~LagerObjekt()
{
}
