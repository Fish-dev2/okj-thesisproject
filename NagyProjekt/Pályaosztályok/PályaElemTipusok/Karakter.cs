using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NagyProjekt
{
    class Karakter : PályaElem
    {
        //olyan objektumok a pályán amik képesek mozgásra
        //van sebességük
        //van sebzésük (akár a játékos felé, akár más felé)
        //zombiknak előre meghatározott sebzése van, mig a játékosnak és a fegyvereinek a fegyvertől függ
        private double sebesség;

        public double Sebesség
        {
            get { return sebesség; }
            set { sebesség = value; }
        }
        private double sebzés;

        public double Sebzés
        {
            get { return sebzés; }
            set { sebzés = value; }
        }
        private Vector irany;

        public Vector Irany
        {
            get { return irany; }
            set { irany = value; }
        }

        private int sebzesDelay;

        public int SebzesDelay
        {
            get { return sebzesDelay; }
            set { sebzesDelay = value; DelayEllenoriz(); }
        }

        private void DelayEllenoriz()
        {
            if (sebzesDelay <= 0)
            {
                sebzesDelay = 100;
            }
        }

        public Karakter(double xHely,double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet, double sebesség, double sebzés, string textura) : base(xHely,yHely,xMéret,yMéret,maxElet,kezdoElet,textura)
        {
            Sebesség = sebesség;
            Sebzés = sebzés;
            sebzesDelay = 100;
            Irany = new Vector();
        }
        public Karakter(double xHely, double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet, double sebesség, double sebzés, string textura,int sebzesDelay) : base(xHely, yHely, xMéret, yMéret, maxElet, kezdoElet, textura)
        {
            Sebesség = sebesség;
            Sebzés = sebzés;
            this.SebzesDelay = sebzesDelay;
            Irany = new Vector();
        }
    }
}
