using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reiseunternehmen;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaktionslogik für Anmeldung.xaml
    /// </summary>
    public partial class Anmeldung : Window
    {
        private Program p;
        public Anmeldung()
        {
            InitializeComponent();
            p = new Program();
            RavenApi.Init();
            //RavenApi.ErstelleDatenbank("Reiseunternehmen");
            RavenApi.FuelleDatenbank(p.Reiseliste);
            RavenApi.FuelleDatenbank(p.Benutzerliste);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Benutzer benutzer = p.Login(anmeldungBenutzernameEingabe.Text, anmeldungPasswortEingabe.Text);

            if(benutzer == null)
            {
                MessageBox.Show("Benutzername oder Passwort falsch");
            }
            else
            {
                MessageBox.Show("Wilkommen " + benutzer.Daten.Vorname + " " + benutzer.Daten.Nachname);
            }
        }
    }
}
