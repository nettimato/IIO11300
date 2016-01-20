using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class OldIkkuna
    {
        // älä käytä, edustaa "huonoa" ohjelmointitapaa
        // QUICK AND DIRTY
        public double leveys, korkeus;
        public double LaskePintaAla() {
            return leveys * korkeus;
        }
    }
    public class Ikkuna {

        // properties
        // property = Ominaisuus
        // parempi tapa on avata olio "hallitusti" ominaisuuksien kautta
        private double korkeus;

        public double Korkeus
        {
            get { return korkeus; }
            set { korkeus = value; }
        }
        private double leveys;

        public double Leveys
        {
            get { return leveys; }
            set { leveys = value; }
        }
        // read-only -tyyppinen property
        public double PintaAla
        {
            get
            {
                return LaskePAla();
            }
        }
        // metodit
        public double LaskePintaAla() {
            return korkeus * leveys;
        }
        // jos tehdään read only propertyllä
        private double LaskePAla()
        {
            return korkeus * leveys;
        }
    }
}
