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
            kinalat.Content = csokik.GroupBy(c => new { c.TermekTipus, c.KakaoTartalom })
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
                                 .Select(c => $"{c.First().TermekNev} {c.Key}")
                                 .ToList();

            File.WriteAllLines(@"..\..\..\src\lista.txt", elerheto);

            //6. feladat
            var kakaos = csokik.Where(c => int.Parse(c.KakaoTartalom) > 0).ToList();

        }
        private void arajanlat_Click(object sender, RoutedEventArgs e)
        {
            //7. feladat

            var keresettTipus = csokik.Select(c => c.TermekTipus == keresettCsokiTipus.Text).ToList();

            if (keresettTipus != null)
            {
                MessageBox.Show($"{keresettTipus.Count()}db termék van!", "ikd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }

        private void ujTermekFelvetele_Click(object sender, RoutedEventArgs e)
        {
            //8. feladat

            //itt van de nincs befejezve
            //if (termekNeve.Text != string.Empty && int.Parse(termekNeve.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else if (csokiTipusa.Text != string.Empty && int.Parse(csokiTipusa.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else if (termekAra.Text != string.Empty && int.Parse(termekAra.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else if (termekTipusa.Text != string.Empty && int.Parse(termekTipusa.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else if (kakaoTartalom.Text != string.Empty && int.Parse(kakaoTartalom.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else if (nettoTomeg.Text != string.Empty && int.Parse(nettoTomeg.Text) > 0)
            //{
            //    MessageBox.Show("teszt", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}


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