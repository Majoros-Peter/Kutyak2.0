using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kutyak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kutya> kutyak = File.ReadAllLines("Kutyak.csv").Skip(1).Select(G => new Kutya(G.Split(';'), "KutyaNevek.csv", "KutyaFajtak.csv")).ToList();

        public MainWindow()
        {
            InitializeComponent();

            lbKutyaNevekSzama.Content = $"3. feladat: Kutyák száma: {kutyak.DistinctBy(G=>G.GetNev).ToList().Count}";

            lbKutyakAtlagEletkora.Content = $"6. feladat: Kutyák átlag életkora: {Math.Round(kutyak.Average(G=>G.EletKor), 2)}";

            Kutya legidosebb = kutyak.Where(G=>G.EletKor == kutyak.Max(G => G.EletKor)).First();
            lbLegidosebbKutya.Content = $"7. feladat: Legidősebb kutya neve és fajtája: {legidosebb.GetNev.KutyaNev}, {legidosebb.GetFajta.Nev}";

            IGrouping<DateTime, Kutya> legtobbKutyaEgyNap = kutyak.GroupBy(G => G.UtolsoOrvosiEllenorzes).OrderByDescending(G=>G.Count()).First();
            lbLegjobbanTerheltNap.Content = $"9. feladat: Legjobban terhelt nap: {legtobbKutyaEgyNap.Key.ToShortDateString()}: {legtobbKutyaEgyNap.Count()} kutya";
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new()
            {
                Title = "Hova szeretné elmenteni?",
                DefaultExt = "txt"
            };


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)dpVizsgaltKutyaFajtak.SelectedDate;
            try
            {
                lbVizsgaltKutyaFajtak.ItemsSource = kutyak.Where(G => G.UtolsoOrvosiEllenorzes == date).GroupBy(G => G.GetFajta.Nev).Select(G => $"{G.Key}: {G.Count()}").ToList();
            }
            catch
            {
                MessageBox.Show("Nincs adat erről a napról");
            }
        }
    }
}
