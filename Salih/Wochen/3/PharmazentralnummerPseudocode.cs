<obj>list<LagerObjekt*> bestand //global

START function Program.Main()

	while true
		<attr>int aktion
		repeat
		    <attr>aktion = call hauptmenü()
		until <attr>aktion == 0

		case <attr>aktion
		    Wert 1:call nummerPruefen()
                break
            Wert 2: return


END function main

START function Program.hauptmenü()
{
	call system("CLS")
	<attr>string eingabe

	print "Wilkommen im Pharmazentralnummer Pruefer. Was möchten sie tun?\n\n\n"
	
	print "1 - Nummer kontrollieren\n"
	print "2 - Beenden\n"

	print "Ihre Auswahl: "
	
	input to <attr>eingabe

	if call eingabe.All(call char.IsDigit)
		if call int.Parse(<attr>eingabe) <= 4 UND call int.Parse(<attr>eingabe) >= 1
			return call int.Parse(<attr>eingabe)
	return 0
END function hauptmenü

START function Program.nummerPruefen()

    print "Zu Ueberpruefende Nummer eingeben:\n"

    <attr>string nummer = call Console.ReadLine() 

    if <attr>nummer.Length NOT == 8
        print "Die Nummer muss genau 8 Zeichen lang sein\n"
        call Console.ReadKey()
        return

    if NOT call nummer.All(call char.IsDigit))
        print "Die Eingabe darf nur aus Zahlen besteghen\n"
        call Console.ReadKey()
        return

    <attr>int sum = 0
    for <attr>int i = 0 to 7 by 1
        <attr>sum += (call int.Parse(call nummer[i].ToString())) * (<attr>i + 1)

    <attr>int checksum = callint.Parse(<call>nummer[7].ToString())

    <attr>bool check = sum % 11 == checksum  
    if <attr>check==true
        print "Die eingegebene Nummer ist valide\n"
    else 
        print "Die eingegebene Nummer ist nicht valide\n"
    call Console.ReadKey()
