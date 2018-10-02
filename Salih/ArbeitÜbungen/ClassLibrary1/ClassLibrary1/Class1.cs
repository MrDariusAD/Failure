//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace Reiseunternehmen
{

    public class RavenApi
    {
        private static readonly string[] _datenbankUrl = {"http://localhost:8081"};

        private static string _defaultDB = "Reiseunternehmen";

        private static DocumentStore _store = null;

        public static bool Init()
        {

            try
            {
                _store = new DocumentStore
                {
                    Urls = _datenbankUrl,
                    Database = _defaultDB
                };
                _store.Initialize();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public static dynamic OeffneDokument(string dokumentId, string nameDerDatenbank)
        {
            try
            {
                using (var session = _store.OpenSession(nameDerDatenbank))
                {
                    var p = session.Load<dynamic>(dokumentId);

                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }

        public static void SpeichereDokument(dynamic dokument)
        {
            using (IDocumentSession session = _store.OpenSession(dokument.GetType().Name))
            {

                session.Store(dokument);
                session.SaveChanges();
            }
        }

        public static void FuelleDatenbank<T>(List<T> objects)
        {
            if (objects.Count == 0) return;
            string typeName = objects[0].GetType().Name;

            ErstelleDatenbank(typeName);

            foreach (dynamic crs in objects)
            {
                SpeichereDokument(crs);
            }
        }

        public static void ErstelleDatenbank(string nameDerDatenbank)
        {
            if(!DieDatenbankExistiert(nameDerDatenbank))_store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(nameDerDatenbank), 1));
        }

        public static bool DieDatenbankExistiert(string nameDerDatenbank)
        {
            string[] datenbanken = _store.Maintenance.Server.Send(new GetDatabaseNamesOperation(0, 10));

            if (datenbanken.Contains(nameDerDatenbank)) return true;
            return false;
        }
    }


    public class Benutzer
    {
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        public Personendaten Daten { get; set; }
        public List<Buchung> Buchungen;
        public double Guthaben { get; set; }

        public Benutzer(string neuerBenutzername, string neuesPasswort, string vorname, string nachname, DateTime geburtstag, int alter)
        {
            Buchungen = new List<Buchung>();
            Benutzername = neuerBenutzername;
            Passwort = neuesPasswort;
            Daten = new Personendaten(vorname, nachname, alter, geburtstag, this);
            Guthaben = 0.0;

        }

        public void PasswortÄndern(string neuesPasswort)
        {
            Passwort = neuesPasswort;
        }

        public void BenutzernameÄndern(string neuerBenutzername)
        {
            Benutzername = neuerBenutzername;
        }

        public double GuthabenÄndern(double betrag)
        {
            Guthaben += (betrag);
            return Guthaben;
        }

        public bool BuchungHinzufügen(Buchung buchung)
        {
            if (buchung.AnzReisende <= buchung.DieReise.AnzPlätze - buchung.DieReise.BelegtePlätze)
            {
                Buchungen.Add(buchung);
                return true;
            }
            else return false;
        }
    }

    public class Buchung
    {
        public Benutzer Bucher { get; set; }
        public int AnzReisende { get; set; }
        public List<Personendaten> Personendaten { get; set; }
        public Reise DieReise { get; set; }

        public Buchung(Benutzer Bucher, int AnzahlReisende, List<Personendaten> Daten, Reise Reise)
        {
            this.Bucher = Bucher;
            this.AnzReisende = AnzahlReisende;
            this.Personendaten = new List<Personendaten>(Daten);
            this.DieReise = Reise;
        }
    }

    public class Personendaten
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int Alter { get; set; }
        public DateTime Geburtsdatum { get; set; }
        //public Benutzer ZugehörigerBenutzer { get; set; }

        public Personendaten(string neuerVorname, string neuerNachname, int neuesAlter, DateTime neuesGebDatum, Benutzer neuerBenutzer)
        {
            this.Vorname = neuerVorname;
            this.Nachname = neuerNachname;
            this.Alter = neuesAlter;
            this.Geburtsdatum = neuesGebDatum;
            //this.ZugehörigerBenutzer = neuerBenutzer;
        }
        public Personendaten(string neuerVorname, string neuerNachname, int neuesAlter, DateTime neuesGebDatum)
        {
            this.Vorname = neuerVorname;
            this.Nachname = neuerNachname;
            this.Alter = neuesAlter;
            this.Geburtsdatum = neuesGebDatum;
            //this.ZugehörigerBenutzer = null;
        }

    }

    public class Reise
    {
        public string Von { get; set; }
        public string Nach { get; set; }
        public double PreisProPerson { get; set; }
        public int AnzPlätze { get; set; }
        public int BelegtePlätze { get; set; }
        public List<Personendaten> Teilnehmer;

        public Reise(string _von, string _nach, double _preisProPerson, int _anzPlätze)
        {
            Von = _von;
            Nach = _nach;
            PreisProPerson = _preisProPerson;
            AnzPlätze = _anzPlätze;
            BelegtePlätze = 0;
            Teilnehmer = new List<Personendaten>();
        }
        void TeilnehmerHinzufügen(Personendaten daten)
        {
            Teilnehmer.Add(daten);
            BelegtePlätze++;
        }



    }
    public class Program
    {
        public List<Benutzer> Benutzerliste;
        public Benutzer AngemeldeterBenutzer;
        public List<Reise> Reiseliste;

        public Program()
        {
            Benutzerliste = new List<Benutzer>();
            AngemeldeterBenutzer = null;
            Reiseliste = new List<Reise>();

            Init();
        }

        public void Init()
        {

            Reiseliste.Add(new Reise("Paris", "London", 350.96, 263));
            Reiseliste.Add(new Reise("London", "New York", 350.96, 23));
            Reiseliste.Add(new Reise("Frankfurt", "Istanbul", 350.96, 63));
            Reiseliste.Add(new Reise("Calden", "Frankfurt", 350.96, 26));
            Benutzerliste.Add(new Benutzer("MrDariusAD", "test", "Salih", "Selvi", new DateTime(1999, 06, 30), 19));

        }

        private void Start()
        {
            Console.Clear();

            HeaderAusgeben();

            Console.WriteLine("Was möchten sie tun?\n");
            Console.WriteLine("1 - Anmelden\n");
            Console.WriteLine("2 - Registrieren\n");
            Console.WriteLine("3 - Schließen\n");

            string _eingabe = Console.ReadKey().KeyChar.ToString();

            switch (_eingabe)
            {
                case "1":
                    //Login();
                    break;
                case "2":
                    Registrierung();
                    break;
                case "3": return;
                default:
                    Console.WriteLine("Ungueltige Eingabe, bitte erneut versuchen");
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Start();
                    break;
            }
        }

        public Benutzer Login(string _benutzername, string _passwort)
        {
            foreach (Benutzer crs in Benutzerliste)
            {
                if (crs.Benutzername == _benutzername)
                {
                    AngemeldeterBenutzer = crs;
                    break;
                }
            }

            if (AngemeldeterBenutzer != null)
            {
                if (_passwort == AngemeldeterBenutzer.Passwort)
                {
                    return AngemeldeterBenutzer;
                }
                else
                {
                    AngemeldeterBenutzer = null;
                }
            }

            return AngemeldeterBenutzer;
        }

        public void Hauptmenü()
        {
            Console.Clear();
            HeaderAusgeben();

            Console.WriteLine("Ihr Guthaben: " + AngemeldeterBenutzer.Guthaben.ToString() + "\n");

            Console.WriteLine("Was möchten sie tun?\n");
            Console.WriteLine("1 - Reise buchen\n");
            Console.WriteLine("2 - Meine Reisen\n");
            Console.WriteLine("3 - Reisen ansehen\n");
            Console.WriteLine("4 - Passwort aendern\n");
            Console.WriteLine("5 - Abmelden\n");
            Console.WriteLine("6 - [DEBUG]Guthaben Aufladen\n");


            string _eingabe = Console.ReadKey().KeyChar.ToString();

            switch (_eingabe)
            {
                case "1":
                    ReiseBuchen();
                    break;
                case "2":
                    MeineReisen();
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Hauptmenü();
                    break;
                case "3":
                    ReisenAusgeben();
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Hauptmenü();
                    return;
                case "4":
                    PasswortÄndern();
                    Hauptmenü();
                    return;
                case "5":
                    Abmelden();
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Hauptmenü();
                    return;
                case "6":
                    GuthabenAufladen();
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Hauptmenü();
                    return;
                default:
                    Console.WriteLine("Ungueltige Eingabe, bitte erneut versuchen");
                    Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                    Console.ReadKey();
                    Hauptmenü();
                    break;
            }

        }

        public void Registrierung()
        {
            Console.Clear();
            HeaderAusgeben();

            Console.WriteLine("Registrierung\n");
            Console.WriteLine("Benutzername: ");
            string _benutzername = Console.ReadLine();
            Console.WriteLine("\nPasswort: ");
            string _passwort = Console.ReadLine();
            Console.WriteLine("\nPasswort bestätigen: ");
            string _passwort2 = Console.ReadLine();

            string _vorname, _nachname;
            DateTime _gebDatDateTime;
            int _alter;
            PersonendatenErfassen(out _vorname, out _nachname, out _gebDatDateTime, out _alter);

            Benutzerliste.Add(new Benutzer(_benutzername, _passwort, _vorname, _nachname, _gebDatDateTime, _alter));

            Console.Clear();

            Console.WriteLine("Registrierung abgeschlossen");
            Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
            Console.ReadKey();
            Start();

        }

        private void PersonendatenErfassen(out string _vorname, out string _nachname, out DateTime _gebDatDateTime, out int _alter)
        {
            Console.WriteLine("\nGeburtsdatum (TT-MM-YYYY): ");
            string _gebDat = Console.ReadLine();
            Console.WriteLine("\nVorname: ");
            _vorname = Console.ReadLine();
            Console.WriteLine("\nNachname: ");
            _nachname = Console.ReadLine();
            string _gebDatTrennzeichen = "";

            while (!(_gebDat.Length >= 8 && _gebDat.Length <= 10))
            {
                Console.WriteLine("\nGeburtsdatum fehlerhaft. Bitte verwende folgendes Format: TT-MM-YYYY. Mögliche Trennzeichen: '-', '.', '/', '_', ' '");
                Console.WriteLine("\nGeburtsdatum (TT-MM-YYYY): ");
                _gebDat = Console.ReadLine();
            }

            if (_gebDat.Contains("-")) _gebDatTrennzeichen = "-";
            else if (_gebDat.Contains(".")) _gebDatTrennzeichen = ".";
            else if (_gebDat.Contains("/")) _gebDatTrennzeichen = "/";
            else if (_gebDat.Contains("_")) _gebDatTrennzeichen = "_";
            else if (_gebDat.Contains(" ")) _gebDatTrennzeichen = " ";


            _gebDatDateTime = DateTime.ParseExact(_gebDat, "dd" + _gebDatTrennzeichen + "MM" + _gebDatTrennzeichen + "yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            _alter = DateTime.Today.Year - _gebDatDateTime.Year;
            if (_gebDatDateTime > DateTime.Today.AddYears(-_alter)) _alter--;
        }

        public void HeaderAusgeben()
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
            Console.WriteLine("                           Reiseunternehmen Hildegard                      \n");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n\n");
            return;
        }

        public void ReisenAusgeben()
        {
            Console.Clear();
            HeaderAusgeben();

            var _leerzeichenAnzNr = 5;
            var _leerzeichenAnzVon = 20;
            var _leerzeichenAnzNach = 21;
            var _leerzeichenAnzPreis = 9;
            var _leerzeichenAnzBelegtePlätze = 14;

            Console.WriteLine("| Nr. |        Von         |        Nach         |  Preis  |Belegte Plätze|");
            Console.WriteLine("|-----|--------------------|---------------------|---------|--------------|");

            foreach (Reise crs in Reiseliste)
            {

                int _leerzeichen = (_leerzeichenAnzNr - Reiseliste.IndexOf(crs).ToString().Length) / 2;
                bool _leerzeichenUngerade = (_leerzeichenAnzNr - Reiseliste.IndexOf(crs).ToString().Length) % 2 == 1;

                Console.Write("|");

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(Reiseliste.IndexOf(crs).ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzVon - crs.Von.Length) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzVon - crs.Von.Length) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.Von);

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzNach - crs.Nach.Length) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzNach - crs.Nach.Length) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.Nach);

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzPreis - crs.PreisProPerson.ToString().Length) / 2;
                _leerzeichenUngerade = ((_leerzeichenAnzPreis - crs.PreisProPerson.ToString().Length) / 2) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.PreisProPerson.ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzBelegtePlätze - (1 + (crs.AnzPlätze.ToString().Length) + (crs.BelegtePlätze.ToString().Length))) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzBelegtePlätze - (1 + (crs.AnzPlätze.ToString().Length) + (crs.BelegtePlätze.ToString().Length))) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.BelegtePlätze.ToString());
                Console.Write("/");
                Console.Write(crs.AnzPlätze.ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|\n");

            }

            Console.WriteLine("|-------------------------------------------------------------------------|");

        }

        public void ReiseBuchen()
        {
            Console.Clear();
            HeaderAusgeben();

            ReisenAusgeben();

            Console.Write("\n\nBitte geben sie die Reisenummer der Reise ein, die sie buchen möchten: ");
            int _eingabeReisenummer;

            _eingabeReisenummer = int.Parse(Console.ReadLine());

            Console.Clear();


            Console.WriteLine("Ihre Auswahl: Reise von " + Reiseliste[_eingabeReisenummer].Von + " nach " + Reiseliste[_eingabeReisenummer].Nach + " zum Preis von " + Reiseliste[_eingabeReisenummer].PreisProPerson.ToString() + "€ pro Person");
            Console.Write("\nFür wie viele Personen möchten sie buchen (inklusive sie selbst): ");
            int _eingabeAnzPersonen = int.Parse(Console.ReadLine());


            if (Reiseliste[_eingabeReisenummer].BelegtePlätze + _eingabeAnzPersonen > Reiseliste[_eingabeReisenummer].AnzPlätze)
            {
                Console.Clear();
                Console.WriteLine("Leider sind in dieser Reise nicht mehr genug Plätze verfügbar. \nDrücken sie eine beliebige Taste um zurück ins Hauptmenü zu gelangen");
                Console.ReadKey();
                Hauptmenü();
            }
            if (AngemeldeterBenutzer.Guthaben < (_eingabeAnzPersonen * Reiseliste[_eingabeReisenummer].PreisProPerson))
            {
                Console.Clear();
                Console.WriteLine("Ihr Guthaben reicht leider nicht aus, um die von ihnen gewählte Reise zu buchen. \nDrücken sie eine beliebige Taste um zurück ins Hauptmenü zu gelangen");
                Console.ReadKey();
                Hauptmenü();
            }


            Console.Clear();
            Console.WriteLine("Ihre Auswahl: Reise von " + Reiseliste[_eingabeReisenummer].Von + " nach " + Reiseliste[_eingabeReisenummer].Nach + " zum Preis von " + Reiseliste[_eingabeReisenummer].PreisProPerson.ToString() + "€ pro Person für " + _eingabeAnzPersonen.ToString() + " Personen");

            List<Personendaten> personendaten = new List<Personendaten>
            {
                AngemeldeterBenutzer.Daten
            };

            if (_eingabeAnzPersonen > 1)
            {

                for (int i = 1; i < _eingabeAnzPersonen; i++)
                {
                    Console.Clear();
                    Console.WriteLine("\nNun brauchen wir die Daten ihrer Mitreisenden. Bitte geben sie nun die Daten ihrer Mitreisenden an.");
                    Console.WriteLine("\nMitreisender Nummer " + i.ToString());
                    string _vorname, _nachname;
                    DateTime _gebDatDateTime;
                    int _alter;
                    PersonendatenErfassen(out _vorname, out _nachname, out _gebDatDateTime, out _alter);
                    personendaten.Add(new Personendaten(_vorname, _nachname, _alter, _gebDatDateTime));
                }
            }

            Console.Clear();

            Console.WriteLine("Ihre Daten werden übertragen und gespeichert... Bitte warten");

            AngemeldeterBenutzer.BuchungHinzufügen(new Buchung(AngemeldeterBenutzer, _eingabeAnzPersonen, personendaten, Reiseliste[_eingabeReisenummer]));
            AngemeldeterBenutzer.GuthabenÄndern(-1 * (_eingabeAnzPersonen * Reiseliste[_eingabeReisenummer].PreisProPerson));

            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Ihre Buchung wurde übertragen und gespeichert!");

            foreach (Personendaten crs in personendaten)
            {
                Reiseliste[_eingabeReisenummer].Teilnehmer.Add(crs);
                Reiseliste[_eingabeReisenummer].BelegtePlätze++;
            }

            System.Threading.Thread.Sleep(3000);
            Hauptmenü();
        }

        public void MeineReisen()
        {
            Console.Clear();
            HeaderAusgeben();

            var _leerzeichenAnzNr = 5;
            var _leerzeichenAnzVon = 20;
            var _leerzeichenAnzNach = 21;
            var _leerzeichenAnzPreis = 13;
            var _leerzeichenAnzBelegtePlätze = 10;

            Console.WriteLine("| Nr. |        Von         |        Nach         |  Ges.Preis  |Geb. Pltz |");
            Console.WriteLine("|-----|--------------------|---------------------|-------------|----------|");

            foreach (Buchung crs in AngemeldeterBenutzer.Buchungen)
            {

                int _leerzeichen = (_leerzeichenAnzNr - AngemeldeterBenutzer.Buchungen.IndexOf(crs).ToString().Length) / 2;
                bool _leerzeichenUngerade = (_leerzeichenAnzNr - AngemeldeterBenutzer.Buchungen.IndexOf(crs).ToString().Length) % 2 == 1;

                Console.Write("|");

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(AngemeldeterBenutzer.Buchungen.IndexOf(crs).ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzVon - crs.DieReise.Von.Length) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzVon - crs.DieReise.Von.Length) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.DieReise.Von);

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzNach - crs.DieReise.Nach.Length) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzNach - crs.DieReise.Nach.Length) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.DieReise.Nach);

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzPreis - (crs.AnzReisende * crs.DieReise.PreisProPerson).ToString().Length) / 2;
                _leerzeichenUngerade = ((_leerzeichenAnzPreis - (crs.AnzReisende * crs.DieReise.PreisProPerson).ToString().Length) / 2) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write((crs.AnzReisende * crs.DieReise.PreisProPerson).ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|");

                /*-----------------------------------------------------*/

                _leerzeichen = (_leerzeichenAnzBelegtePlätze - (crs.AnzReisende.ToString().Length)) / 2;
                _leerzeichenUngerade = (_leerzeichenAnzBelegtePlätze - (crs.AnzReisende.ToString().Length)) % 2 == 1;

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                Console.Write(crs.AnzReisende.ToString());

                for (int i = _leerzeichen; i > 0; i--)
                {
                    Console.Write(" ");
                }

                if (_leerzeichenUngerade) Console.Write(" ");
                Console.Write("|\n");

            }

            Console.WriteLine("|-------------------------------------------------------------------------|");



        }

        public void PasswortÄndern()
        {
            Console.Clear();
            HeaderAusgeben();

            Console.WriteLine("Altes Passwort: ");
            string _altesPasswort = Console.ReadLine();
            Console.WriteLine("\nNeues Passwort: ");
            string _neuesPasswort = Console.ReadLine();
            Console.WriteLine("\nNeues Passwort bestätigen: ");
            string _neuesPasswortBestätigen = Console.ReadLine();


            if (_altesPasswort != AngemeldeterBenutzer.Passwort)
            {
                Console.WriteLine("Passwort Änderrung fehlgeschlagen. Dein altes Passwort stimmt nicht");
                Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                Console.ReadKey();
            }
            else if (_neuesPasswort != _neuesPasswortBestätigen)
            {
                Console.WriteLine("Passwort Änderrung fehlgeschlagen. Die Passwort bestätigung war fehlerhaft.");
                Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                Console.ReadKey();
            }
            else
            {
                AngemeldeterBenutzer.PasswortÄndern(_neuesPasswort);
                Console.WriteLine("Passwort geändert.");
                Console.WriteLine("\nZum fortfahren eine beliebige Taste drücken");
                Console.ReadKey();
            }

        }

        public void Abmelden()
        {
            AngemeldeterBenutzer = null;
            Console.Clear();
            Console.WriteLine("Abmeldung erfolgreich!");
            Thread.Sleep(3000);

            Start();
        }

        public void GuthabenAufladen()
        {
            Console.WriteLine("Aufzuladender Betrag: ");
            double _aufzuladenderBetrag = Convert.ToDouble(Console.ReadLine());
            AngemeldeterBenutzer.GuthabenÄndern(_aufzuladenderBetrag);

            Console.Clear();
            Console.WriteLine("Guthaben aufgeladen!");
            Thread.Sleep(3000);

            Hauptmenü();
        }
    }
}