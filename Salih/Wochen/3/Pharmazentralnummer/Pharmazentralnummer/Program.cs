using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmazentralnummer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int aktion;
                do
                {
                    aktion = hauptmenü();
                } while (aktion == 0);

                switch (aktion)
                {
                    case 1:
                        nummerPruefen();
                        break;
                    case 2: return;
                };
            }


        }

        private static void nummerPruefen()
        {
            Console.WriteLine("Zu Ueberpruefende Nummer eingeben:");
            string nummer = Console.ReadLine();

            if (nummer.Length != 8)
            {
                Console.WriteLine("Die Nummer muss genau 8 Zeichen lang sein");
                Console.ReadKey();
                return;
            }
            if (!nummer.All(char.IsDigit))
            {
                Console.WriteLine("Die Eingabe darf nur aus Zahlen besteghen");
                Console.ReadKey();
                return;
            }

            int sum = 0;
            for (int i = 0; i < 7; i++)
            {
                sum += (int.Parse(nummer[i].ToString())) * (i + 1);
            }

            int checksum = int.Parse(nummer[7].ToString());
            bool check = sum % 11 == checksum;

            if (check) Console.WriteLine("Die eingegebene Nummer ist valide");
            else Console.WriteLine("Die eingegebene Nummer ist nicht valide");

            Console.ReadKey();

        }

        static int hauptmenü()
        {
            Console.Clear();
            string eingabe;

            Console.Write("Wilkommen im Pharmazentralnummer Pruefer. Was möchten sie tun?\n\n\n");

            Console.Write("1 - Nummer kontrollieren\n");
            Console.Write("2 - Beenden\n\n");

            Console.Write("Ihre Auswahl: ");

            eingabe = Console.ReadLine();

            if (eingabe.All(char.IsDigit))
            {
                if (int.Parse(eingabe) <= 2 && int.Parse(eingabe) >= 1)
                {
                    return int.Parse(eingabe);
                }
            }

            return 0;
        }
    }
}