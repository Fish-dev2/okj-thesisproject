using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagyProjekt.Eroforrasok
{
    class Fegyver : Bindable
    {
        //tartalmazza, hogy hány golyót és milyen golyókat kell létrehoznia a játéknak
        //ötletek:
        //Kezdőpisztoly: lassú, végtelen lőszer, kevés sebzés
        //Shotgun: lassú, sok golyó egyszerre, kicsi sebzés/golyó
        //Sniper: nagyon lassú, nagy sebzés/golyó
        //RPG: nagyon lassú, nagy sebzés/golyó, nagy golyó
        //LMG kontrollálhatatlan, összevissza lő, de nagyon sokat nagyon gyorsan

        //ha -1 a tulajdonság akkor nem igaz rá, ha annál nagyobb szám akkor a szám határozza meg az eltérés mértékét

        private double specialsized; //olyan fegyverek amiknek különleges golyóméretük van
        public double Specialsized
        {
            get { return specialsized; }
            set { specialsized = value; }
        }

        private double randomized; //olyan fegyverek amiknek a golyói eltérhetnek a lövés irányától
        public double Randomized
        {
            get { return randomized; }
            set { randomized = value; }
        }

        private double multishot;
        public double Multishot //olyan fegyver ami egyszerre több golyót lőhet 1 lövéssel
        {
            get { return multishot; }
            set { multishot = value; }
        }

        private double golyoSebesseg;
        public double GolyoSebesseg
        {
            get { return golyoSebesseg; }
            set { golyoSebesseg = value; }
        }

        private double sebzes;
        public double Sebzes
        {
            get { return sebzes; }
            set { sebzes = value; }
        }

        private string golyoTextura;//ha null a string tartalma akkor nincs
        public string GolyoTextura
        {
            get { return golyoTextura; }
            set { golyoTextura = value; }
        }

        private double lovesiSebesseg;
        public double LovesiSebesseg
        {
            get { return lovesiSebesseg; }
            set { lovesiSebesseg = value; }
        }

        private string fegyverNev;
        public string FegyverNev
        {
            get { return fegyverNev; }
            set { fegyverNev = value; TulajdonsagValtozott(); }
        }

        private int loszer;//ha -1 akkor végtelen

        public int Loszer
        {
            get { return loszer; }
            set { loszer = value; TulajdonsagValtozott(); }
        }

        public Fegyver(string nev)
        {
            specialsized = -1;
            randomized = -1;
            multishot = -1;
            golyoSebesseg = 3;
            sebzes = 1;
            golyoTextura = "bullet";
            lovesiSebesseg = 3;
            fegyverNev = nev;
            loszer = 100;

        }
        public Fegyver(double golyoSebesseg,double sebzes, string golyoTextura, double lovesiSebesseg, string fegyverNev, int loszer)
        {
            specialsized = -1;
            randomized = -1;
            multishot = -1;
            this.golyoSebesseg = golyoSebesseg;
            this.sebzes = sebzes;
            this.golyoTextura = golyoTextura;
            this.lovesiSebesseg = lovesiSebesseg;
            this.fegyverNev = fegyverNev;
            this.loszer = loszer;

        }
        public Fegyver(double specialsized, double randomized, double multishot, double golyoSebesseg, double sebzes, string golyoTextura, double lovesiSebesseg, string fegyverNev, int loszer)
        {
            this.specialsized = specialsized;
            this.randomized = randomized;
            this.multishot = multishot;
            this.golyoSebesseg = golyoSebesseg;
            this.sebzes = sebzes;
            this.golyoTextura = golyoTextura;
            this.lovesiSebesseg = lovesiSebesseg;
            this.fegyverNev = fegyverNev;
            this.loszer = loszer;

        }


    }
}
