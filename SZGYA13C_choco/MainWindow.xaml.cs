using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SZGYA13C_choco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Choco> csokik = new List<Choco>();
        public MainWindow()
        {
            InitializeComponent();

            csokik = Choco.FromFile(@"..\..\..\src\choco.txt");

            //2. feladat
            kinalat.Content = csokik.Select(c => c.TermekTipus)
                                    .Distinct()
                                    .Count();

            //3. feladat
            var legolcsobb = csokik.OrderBy(c => c.Ár)
                                   .FirstOrDefault();
            legolcsobbTermek.Content = legolcsobb.TermekNev;

            //4. feladat
            int avg = (int)csokik.Average(c => c.Ár);
            var ajanlott = csokik.FirstOrDefault(c => c.Ár == avg) ?? csokik.Where(c => c.Ár >= avg)
                                                                            .OrderBy(c => c.Ár)
                                                                            .First();
            ajanlatunk.Content = $"{ajanlott?.TermekNev}, {ajanlott?.Ár}";

            // 5. feladat
            var elerheto = csokik.GroupBy(c => c.TermekTipus)
                                 .Select(c => $"{c.ElementAt(new Random().Next(0, c.Count())).TermekNev} {c.Key}")
                                 .ToList();

            File.WriteAllLines(@"..\..\..\src\lista.txt", elerheto);

            //6. feladat
            var kakaok = csokik
                .Where(c => int.TryParse(c.KakaoTartalom, out int kakaotartalom) && kakaotartalom > 0)
                .GroupBy(c => c.KakaoTartalom)
                .Select(g => new
                {
                    KakaoTartalom = g.Key,
                    Darabszam = g.Count()
                })
                .ToList();

            FileInfo statCheck = new FileInfo(@"..\..\..\src\stat.txt");

            if (statCheck.Length == 0)
            {
                foreach (var i in kakaok)
                {
                    string[] stat = new string[] { $"{i.KakaoTartalom};{i.Darabszam}db" };
                    File.AppendAllLines(@"..\..\..\src\stat.txt", stat);
                }
            }


        }
        private void arajanlat_Click(object sender, RoutedEventArgs e)
        {
            //7. feladat
            File.WriteAllText(@"..\..\..\src\ajanlott.txt", string.Empty);

            var keresettTipus = csokik.Where(c => c.TermekTipus == keresettCsokiTipus.Text)
                                      .ToList();

            if (keresettCsokiTipus.Text.Length != 0)
            {
                
                MessageBox.Show($"{keresettTipus.Count()}db termék van!", "Sikeres", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach (var i in keresettTipus)
                {
                    string[] ujAjanlott = new string[] { $"{i.TermekNev};{i.Ár};{i.Tömeg}" };
                    File.AppendAllLines(@"..\..\..\src\ajanlott.txt", ujAjanlott);
                }
                
            }
            else
            {
                MessageBox.Show($"Nincs a feltételeknek megfelelő termékünk. Kérjük, módosítsa a választását!", "Sikeres", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }

        private void ujTermekFelvetele_Click(object sender, RoutedEventArgs e)
        {
            //8. feladat

            if (string.IsNullOrWhiteSpace(termekNeve.Text))
            {
                MessageBox.Show("Nincs megadva a termék neve!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(csokiTipusa.Text))
            {
                MessageBox.Show("Nincs megadva a csoki típusa!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(termekAra.Text))
            {
                MessageBox.Show("Nincs megadva a termék ára!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(termekTipusa.Text))
            {
                MessageBox.Show("Nincs megadva a termék típusa!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(kakaoTartalom.Text))
            {
                MessageBox.Show("Nincs megadva a kakaó tartalom!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(nettoTomeg.Text))
            {
                MessageBox.Show("Nincs megadva a nettó tömeg!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            string[] UJTEMREK = new string[] { $"{termekNeve.Text};{csokiTipusa.Text};{termekAra.Text};{termekTipusa.Text};{kakaoTartalom.Text};{nettoTomeg.Text}" };

            File.AppendAllLines(@"..\..\..\src\choco.txt", UJTEMREK);

            termekNeve.Text = string.Empty;
            csokiTipusa.Text = string.Empty;
            termekAra.Text = string.Empty;
            termekTipusa.Text = string.Empty;
            kakaoTartalom.Text = string.Empty;
            nettoTomeg.Text = string.Empty;

        }

      
    }
}