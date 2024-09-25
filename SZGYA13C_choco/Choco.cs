using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace SZGYA13C_choco
{
    internal class Choco
    {
        public string TermekNev { get; set; }
        public string CsokiTipus { get; set; }

        public int Ár { get; set; }
        public string TermekTipus { get; set; }
        public string KakaoTartalom { get; set; } //%
        public int Tömeg { get; set; }

        public Choco (string nev, string csTipus, int ár, string tTipus, string kTartalom, int tömeg)
        {
            TermekNev = nev;
            CsokiTipus = csTipus;
            Ár = ár;
            TermekTipus = tTipus;
            KakaoTartalom = kTartalom;
            Tömeg = tömeg;
        }

        public static List<Choco> FromFile(string path)
        {
            List<Choco> csokik = new List<Choco> ();

            string[] line = File.ReadAllLines(path);

            foreach (var l in line.Skip(1))
            {
                string[] cs = l.Split(';');

                string TermekNev = cs[0];
                string CsokiTipus = cs[1];
                int Ár = int.Parse(cs[2]);
                string TermekTipus = cs[3];
                string KakaoTartalom = cs[4];
                int Tömeg = int.Parse(cs[5]);

                Choco csoki = new Choco(TermekNev, CsokiTipus, Ár, TermekTipus, KakaoTartalom, Tömeg);
                csokik.Add(csoki);

            }
            return csokik;
        }

        public override string ToString()
        {
            return $"{TermekNev}, {CsokiTipus}, {Ár}, {TermekTipus}, {KakaoTartalom}, {Tömeg}"; 
        }
    }
}
