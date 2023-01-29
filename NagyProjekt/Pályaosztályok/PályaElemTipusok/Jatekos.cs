using NagyProjekt.Eroforrasok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagyProjekt
{
    class Jatekos : Karakter
    {
        //játékosnak a tulajdonságai fejleszthetőek:
        //MaxÉlet, Gyógyulás másodpercenként, fegyverek, sebesség,lövési sebesség

        private double lovesiSebessegSzorzo;

        public double LovesiSebesseg
        {
            get { return lovesiSebessegSzorzo; }
            set { lovesiSebessegSzorzo = value; }
        }

        private List<Fegyver> fegyverek;
        public List<Fegyver> Fegyverek
        {
            get { return fegyverek; }
            set { fegyverek = value; }
        }
        private int jelenfegyverid;

        public int JelenFegyverID
        {
            get { return jelenfegyverid; }
            set { jelenfegyverid = value; JelenFegyver = Fegyverek[jelenfegyverid]; }
        }
        private Fegyver jelenFegyver;

        public Fegyver JelenFegyver
        {
            get { return jelenFegyver; }
            set { jelenFegyver = value; TulajdonsagValtozott(); }
        }
        private int penz;

        public int Penz
        {
            get { return penz; }
            set { penz = value; TulajdonsagValtozott(); }
        }


        public Jatekos
            (double xHely, double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet, double sebesség, double sebzés,string textura) 
            : base(xHely, yHely, xMéret, yMéret, maxElet, kezdoElet, sebesség, sebzés,textura)
        {
            Fegyverek = new List<Fegyver>();
            LovesiSebesseg = 1;
            penz = 0;
        }

        public Jatekos() : base(0, 0, 0, 0, 0, 0, 0, 0,"")
        {
            //üres játékos tesztelésre
        }

        internal void FegyverValtas(int v)
        {
            if (v + JelenFegyverID >= Fegyverek.Count())
            {
                JelenFegyverID = 0;
            }
            else if (JelenFegyverID + v < 0)
            {
                JelenFegyverID = Fegyverek.Count() - 1;
            }
            else
            {
                JelenFegyverID += v;
            }
        }
    }
}
