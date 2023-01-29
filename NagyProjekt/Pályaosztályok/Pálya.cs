using NagyProjekt.Pályaosztályok;
using NagyProjekt.Pályaosztályok.PályaElemTipusok;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NagyProjekt
{
    enum JatekStatusz
    {
        WaveKezd,
        WaveMegy,
        WaveVege,
        Megallitva,
        Vasarlas,
        Elvesztes
        
    }
    class Pálya : Bindable
    {
        //az egész pályát tartalmazó osztály, beolvasó és mentési metódusokkal
        private ObservableCollection<PályaElem> pályaElemek;

        public ObservableCollection<PályaElem> PályaElemek
        {
            get { return pályaElemek; }
            set { pályaElemek = value; TulajdonsagValtozott(); }
        }

        private Vector palyaMeretPixelben;
            
        public Vector PalyaMeretPixelben
        {
            get { return palyaMeretPixelben; }
            set { palyaMeretPixelben = value; }
        }

        private Vector gridMeret;

        public Vector GridMeret
        {
            get { return gridMeret; }
            set { gridMeret = value; }
        }
        private Rectangle talaj;

        public Rectangle Talaj
        {
            get { return talaj; }
            set { talaj = value; TulajdonsagValtozott(); }
        }
        private MIkezelo miKezelo;

        public MIkezelo MIkezelo
        {
            get { return miKezelo; }
            set { miKezelo = value; }
        }
        private Jatekos jatekos;

        public Jatekos Jatekos
        {
            get { return jatekos; }
            set { jatekos = value; }
        }

        public Pálya(string pályaFájl,Jatekos jatekos)
        {
            miKezelo = new MIkezelo();
            PályaElemek = new ObservableCollection<PályaElem>();
            Jatekos = jatekos;
            PályaElemek.Add(jatekos);
            PályaBeolvas(("Eroforrasok/Palyak/" + pályaFájl));

        }

        private void PályaBeolvas(string fajlhelye)
        {
            string [] mapfajlnyers = File.ReadAllLines(fajlhelye);
            string[] palyameretsor = mapfajlnyers[2].Split('*');
            PalyaMeretPixelben = new Vector(Convert.ToDouble(palyameretsor[0]), Convert.ToDouble(palyameretsor[1]));
            TalajHozzaAd(mapfajlnyers[1]);
            GridMeretMeghataroz(mapfajlnyers[0]);
            PályaElemekHozzáad(mapfajlnyers);


        }

        private void PályaElemekHozzáad(string[] mapfajlnyers)
        {
            for (int i = 3; i < mapfajlnyers.Length; i++)
            {
                string[] sor = mapfajlnyers[i].Split('(');
                if (sor[0] == "OBJ")
                {
                    ObjektumHozzáad(sor[1]);
                }
            }
        }

        private void ObjektumHozzáad(string sor)
        {
            string[] adatok = sor.Split(',');
            PályaElemek.Add(new Objektum(Convert.ToDouble(adatok[0]), Convert.ToDouble(adatok[1]), Convert.ToDouble(adatok[2]), Convert.ToDouble(adatok[3]), Convert.ToDouble(adatok[4]), Convert.ToDouble(adatok[5]), adatok[6]));
            
        }

        private void GridMeretMeghataroz(string s)
        {
            string[] meretek = s.Split('*');
            GridMeret = new Vector(palyaMeretPixelben.X / double.Parse(meretek[0]), palyaMeretPixelben.Y / double.Parse(meretek[1]));

        }

        private void TalajHozzaAd(string s)
        {
            PályaElem pályaElem = new PályaElem(0, 0, PalyaMeretPixelben.X, PalyaMeretPixelben.Y, -1, -1, s);
            pályaElem.Modell.Tag = "talaj";
            Panel.SetZIndex(pályaElem.Modell, -2);
            pályaElem.Modell.Name = "talaj";
            PályaElemek.Add(pályaElem);


            Talaj = new Rectangle();
            Binding myBind = new Binding("Hely.X");
            myBind.Source = pályaElem;
            Talaj.SetBinding(Canvas.LeftProperty, myBind);

            myBind = new Binding("Hely.Y");
            myBind.Source = pályaElem;
            Talaj.SetBinding(Canvas.TopProperty, myBind);

            myBind = new Binding("Meret.X");
            myBind.Source = pályaElem;
            Talaj.SetBinding(FrameworkElement.WidthProperty, myBind);

            myBind = new Binding("Meret.Y");
            myBind.Source = pályaElem;
            Talaj.SetBinding(FrameworkElement.HeightProperty, myBind);
        }

        public List<PályaElem> MozgatElemek()
        {
            List<PályaElem> elemToTorol = new List<PályaElem>();
            for (int i = 0; i < PályaElemek.Count(); i++)
            {
                PályaElem elem = PályaElemek[i];
                bool LehetMozgatni = true;
                if (!(elem is Karakter))
                {
                    LehetMozgatni = false;
                }

                if (LehetMozgatni)
                {
                    Karakter karakter = (Karakter)elem;
                    if (karakter.Hely.X + karakter.Meret.X + karakter.Irany.X*2 > Canvas.GetLeft(Talaj) + Talaj.Width ||
                        karakter.Hely.X + karakter.Irany.X*2 < Canvas.GetLeft(Talaj) ||
                        karakter.Hely.Y + karakter.Meret.Y + karakter.Irany.Y*2 > Canvas.GetTop(Talaj) + Talaj.Height ||
                        karakter.Hely.Y + karakter.Irany.Y*2 < Canvas.GetTop(Talaj))
                    {
                        //ha az adott elem kimenne a pályáról akkor nem lehet mozgatni
                        LehetMozgatni = false;
                    }
                    if (LehetMozgatni)
                    {
                        Rect mozgattester = new Rect(elem.Hely.X + karakter.Irany.X, elem.Hely.Y + karakter.Irany.Y, elem.Meret.X, elem.Meret.Y);
                        Rect utkoztester = new Rect();
                        for (int j = 0; j < PályaElemek.Count(); j++)
                        {
                            if (elem.GetType() != PályaElemek[j].GetType() && !(PályaElemek[j] is Golyo)) //ugyanolyan tipusu dolgok átmehetnek egymáson, vizsgálni kell hogy nem golyó amivel ütközik mert a sebzéseket a golyók kezelik
                            {
                                if (i != j && (pályaElemek[j].Modell.Tag == null || pályaElemek[j].Modell.Tag.ToString() != "talaj")) //ne legyen önmaga és a talajt ne vizsgálja
                                {
                                    utkoztester = new Rect(pályaElemek[j].Hely.X, pályaElemek[j].Hely.Y, pályaElemek[j].Meret.X, pályaElemek[j].Meret.Y);
                                    if (mozgattester.IntersectsWith(utkoztester))
                                    {
                                        //ha ütközött akkor a megfelelő eljárások elvégzése az ütköző és az ütközött között
                                        //ütközés vizsgálat más elemekkel
                                        bool ezObjektum = false;
                                        Objektum objekt = null;
                                        if (PályaElemek[j] is Objektum)
                                        {
                                            ezObjektum = true;
                                            objekt = (Objektum)PályaElemek[j];

                                        }
                                        if (elem is Golyo)
                                        {
                                            if (!(PályaElemek[j] is Jatekos) && (!ezObjektum || !objekt.GolyoAtmegy))//Játékossal nem ütközhet, bizonyos objektumokon átmehet a golyó
                                            {
                                                Golyo golyo = (Golyo)elem;
                                                elemToTorol.Add(golyo);
                                                PályaElemek[j].EletPont.Sebzodik(golyo.Sebzés);
                                                if (PályaElemek[j].EletPont.JelenErtek == 0)
                                                {
                                                    //ha a sebzéstől meghal akkor kerüljön a törölt dolgok közé
                                                    if (!elemToTorol.Contains(PályaElemek[j]))
                                                    {
                                                        elemToTorol.Add(PályaElemek[j]);
                                                    }
                                                }
                                                LehetMozgatni = false;
                                                j = PályaElemek.Count();
                                                //ha golyó akkor törlése kerül ha nekiütközik bárminek
                                            }
                                        }
                                        else if (elem is Jatekos)
                                        {
                                            if (ezObjektum)
                                            {
                                                LehetMozgatni = false;
                                            }
                                        }
                                        else if(elem is Karakter)
                                        {
                                            LehetMozgatni = false;
                                            if (karakter.SebzesDelay == 100 && (PályaElemek[j] is Jatekos || PályaElemek[j] is Objektum))
                                            {
                                                karakter.SebzesDelay = karakter.SebzesDelay - 1;
                                                PályaElemek[j].EletPont.Sebzodik(karakter.Sebzés);
                                                if (PályaElemek[j].EletPont.JelenErtek == 0)
                                                {
                                                    //ha a sebzéstől meghal akkor kerüljön a törölt dolgok közé
                                                    if (!elemToTorol.Contains(PályaElemek[j]))
                                                    {
                                                        elemToTorol.Add(PályaElemek[j]);
                                                    }
                                                }
                                            }
                                            if (PályaElemek[j] is Jatekos)
                                            {
                                                LehetMozgatni = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (karakter.SebzesDelay != 100)
                        {
                            karakter.SebzesDelay = karakter.SebzesDelay - 1;
                        }

                        //CIKLUS VÉGE
                        if (LehetMozgatni)
                        {
                            karakter.Hely = new Vector(karakter.Hely.X + karakter.Irany.X, karakter.Hely.Y + karakter.Irany.Y);
                        }
                    }
                    else if(elem is Golyo)
                    {
                        //golyó elhagyja a pályát akkor törlésre kerül, minden más pályaelhagyás esetén nem tud mozogni, kivéve játékos
                        elemToTorol.Add(elem);
                    }
                }

                if (PályaElemek[i] is Karakter && !(PályaElemek[i] is Jatekos) && !(PályaElemek[i] is Golyo))
                {
                    Karakter karakter = (Karakter)PályaElemek[i];
                    karakter.Irany = miKezelo.IrányVisszaad(karakter.Hely, Jatekos.Hely);
                    karakter.Modell = ForgatKarakter(karakter);
                }
            }
            //MÁSIK CIKLUS VÉGE
            foreach (var item in elemToTorol)
            {
                //visszaadni a törölt elemek listáját
                PályaElemek.Remove(item);

            }
            return elemToTorol;
        }

        private Rectangle ForgatKarakter(Karakter karakter)
        {
            Point origo = new Point(0,0);
            Point egerPoz = new Point(karakter.Irany.X, karakter.Irany.Y);
            egerPoz.X -= origo.X;
            egerPoz.Y -= origo.Y;
            double test2 = Math.Atan2(egerPoz.Y, egerPoz.X);
            double angle2 = test2 * 180.00 / Math.PI;
            RotateTransform rotateTransform = new RotateTransform(angle2);
            Rectangle ret = karakter.Modell;
            ret.LayoutTransform = rotateTransform;
            return ret;

        }
    }
}
