<obj>list<LagerObjekt*> bestand //global

START function main()

	call <obj>bestand.push_back((new LagerObjekt("Beispiel1", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel2", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel3", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel4", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel5", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel6", 5, 12.0, LagerObjekt::typ::Verkauf)))
	call <obj>bestand.push_back((new LagerObjekt("Beispiel7", 5, 12.0, LagerObjekt::typ::Verkauf)))

	while true
		int aktion
		repeat
		    <attr>aktion = call hauptmenü()
		until <attr>aktion == 0

		case <attr>aktion
		    Wert 1:call einkauf()
			break
		    Wert 2: call verkauf()
			break
		    Wert 3: call bestandsliste(true)
			break
END function main

START function hauptmenü()
{
	call system("CLS")
	<attr>string eingabe

	print "Wilkommen im Lager Management Programm. Was möchten sie tun?\n\n\n"
	
	print "1 - Einkauf\n"
	print "2 - Verkauf\n"
	print "3 - Bestandsausgabe\n"
	print "4 - Beenden\n"

	print "Ihre Auswahl: "
	
	input to <attr>eingabe

	if call is_number(<attr>eingabe)
		if call stoi(<attr>eingabe) <= 4 UND call stoi(<attr>eingabe) >= 1
			return call stoi(<attr>eingabe)
	return 0

END function hauptmenü

START function is_number(<attr>const std::string& s)
	<attr>std::string::const_iterator it = call s.begin()
	while it NICHT == call s.end() UND call std::isdigit(*it) it = it + 1
	return NICHT call s.empty() UND it == call s.end()
END function is_number

START function einkauf()
	<attr>string newName = ""
	<attr>int newAnzahl = 0
	<attr>double newPreis = 0.0
	<attr>int t

	while (1)
		system("CLS")
		print "Name: "
		getline(std::cin >> std::ws, <attr>newName)
		if <attr>newName == "stop" ODER <attr>newName == "STOP" ODER <attr>newName == "Stop"
            return
		print "\nAnzahl: "
		input to <attr>newAnzahl
		print "\nPreis: "
		input to <attr>newPreis
		print "\nTyp(0=Verkauf, 1=Eigenbedarf): "
		input to <attr>t
		call <obj>bestand.push_back((new LagerObjekt(newName, newAnzahl, newPreis, (LagerObjekt::typ)t)))

	return
END function einkauf

START function verkauf()
	<attr>string eingabe
	<attr>int IEingabe
	<attr>string anzahl
	<attr>int IAnzahl
	call bestandsliste(false)
	print "\nWelches Objekt wird verkauft? Nr. "

	input to eingabe

	if call is_number(eingabe)
		<attr>IEingabe = call stoi(<attr>eingabe)

	print "\n\nWie viele Objekte werden verkauft? : "

	input to <attr>anzahl

	if call is_number(<attr>anzahl)
		<attr>IAnzahl = call stoi(<attr>anzahl)

	if <attr>IAnzahl > call getListObject(<attr>IEingabe)-><attr>anzahl
        error throw new <obj>exception("Anzahl der zu verkaufenden Objekte kann nicht größer als der aktuelle Bestand sein")
	
	call getListObject(<attr>IEingabe)-><attr>anzahl = call getListObject(<attr>IEingabe)-><attr>anzahl-<attr>IAnzahl
END function verkauf

START function bestandsliste(<attr>bool pause)
	call system("CLS")

	print "Nr." + " -- " + "Name" + " -- " + "Anzahl" + " -- " + "Preis" + " -- " + "Typ\n"
	print "--------------------------------------"+'\n'

	<attr>int i = 0

	for each <attr>auto crs in <attr>bestand
		print + i + ". -- " + crs->name + " -- " + crs->anzahl + " -- " + crs->preis + " -- "
		case <attr>crs->t
		<attr>Wert crs->typ::Verkauf: print + "Verkauf"
			break
		Wert <attr>crs->typ::Eigenbedarf: print + "Eigenbedarf"
			break
		default: print + "ERROR"
			break
		print + endl
		i = i + 1
	if pause
        call system("pause")
END function bestandsliste

START function LagerObjekt* getListObject(<attr>int i)
	<attr>int nr = 0
	for each <attr>auto crs in <obj>bestand
		if nr == i
            return crs
		<attr>nr = <attr>nr + 1
	return nullptr
END function getListObject