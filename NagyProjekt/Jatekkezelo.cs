using NagyProjekt.Pályaosztályok.PályaElemTipusok;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using NagyProjekt.Pályaosztályok;
using NagyProjekt.Eroforrasok;
using System.Windows.Data;

namespace NagyProjekt
{
    class Jatekkezelo : Bindable
    {
        private MIkezelo mIKezelo;

        public MIkezelo MIKezelo
        {
            get { return mIKezelo; }
            set { mIKezelo = value; }
        }

        private DispatcherTimer idozito;

        public DispatcherTimer Idozito
        {
            get { return idozito; }
            set { idozito = value; }
        }
        private JatekAblak jatekAblak;

        public JatekAblak JatekAblak
        {
            get { return jatekAblak; }
            set { jatekAblak = value; }
        }
        private Pálya jelenPálya;

        public Pálya JelenPálya
        {
            get { return jelenPálya; }
            set { jelenPálya = value; }
        }
        private ObservableCollection<System.Windows.Input.Key> gombok;

        public ObservableCollection<System.Windows.Input.Key> Gombok
        {
            get { return gombok; }
            set { gombok = value; }
        }
        private bool isMouseDown;

        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set { isMouseDown = value; }
        }

        private Jatekos jatekos;

        public Jatekos Jatekos
        {
            get { return jatekos; }
            set { jatekos = value; TulajdonsagValtozott(); }
        }

        private double gyokKetto;
        Random random;
        private Vector lovesIrany;

        public Vector LovesIrany
        {
            get { return lovesIrany; }
            set { lovesIrany = value; }
        }


        private HangKezelo hangKezelo;

        public HangKezelo HangKezelo
        {
            get { return hangKezelo; }
            set { hangKezelo = value; }
        }

        private WaveKezelo waveKezelo;

        public WaveKezelo WaveKezelo
        {
            get { return waveKezelo; }
            set { waveKezelo = value; }
        }




        public int loveslassito { get; set; }

        public Jatekkezelo(string palyanev,Jatekos jatekos)
        {
            HangKezelo = new HangKezelo();
            MIKezelo = new MIkezelo();
            WaveKezelo = new WaveKezelo();
            random = new Random();
            IdozitoElindit();//idozito feliratkoztatás, értékadás
            JatekAblakElindit();//játékablak létrehozás,esemény feliratkoztatás
            PályaLétrehoz(palyanev, jatekos); //Pálya betöltése a fájlból, játékos hozzáadása
            Gombok = new ObservableCollection<Key>();
            Gombok.CollectionChanged += Gombok_CollectionChanged;
            gyokKetto = 1.4142; //Math.Sqrt(2); helyett
            //1.4142
            TalajHatterHozzaAd();
            JatekSetupMetodus();
            //zeneplayer sfxplayer átvétele
            JatekAblak.pénztext.DataContext = Jatekos;
            JatekAblak.mainstack.Width = JatekAblak.Width;
            JatekAblak.nextwavebutt.MouseLeftButtonDown += Nextwavebutt_MouseLeftButtonDown;
        }

        private void Nextwavebutt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //dobozok lerakása amik ideiglenesen lettek
            WaveKezelo.Statusz = JatekStatusz.WaveKezd;
        }

        private void JatekSetupMetodus()
        {
            var gombok = from x in JatekAblak.balPanel.Children.OfType<Label>()
                         where x.Tag != null && x.Tag.ToString() == "clickable"
                         select x;
            foreach (var gomb in gombok)
            {
                gomb.MouseLeftButtonDown += Gomb_MouseLeftButtonDown;
            }
            gombok = from x in JatekAblak.kozepPanel.Children.OfType<Label>()
                         where x.Tag != null && x.Tag.ToString() == "clickable"
                         select x;
            foreach (var gomb in gombok)
            {
                gomb.MouseLeftButtonDown += Gomb_MouseLeftButtonDown;
            }
            //Kezdőpisztoly: lassú, végtelen lőszer, kevés sebzés
            double specialsized = -1;
            double randomzed = 1;
            double multishot = -1;
            double golyoSebesseg = 20;
            double sebzes = 5;
            double lovesiSebesseg = 75;
            Jatekos.Fegyverek.Add(new Fegyver(specialsized,randomzed,multishot,golyoSebesseg,sebzes, "bullet", lovesiSebesseg, "Pisztoly", -1));
            loveslassito = 0;
            Jatekos.LovesiSebesseg = 1;
            //Shotgun: lassú, sok golyó egyszerre, kicsi sebzés/golyó
            Jatekos.Fegyverek.Add(new Fegyver(specialsized, 10, 5, golyoSebesseg, 5, "bullet", 60, "Shotgun", 0));
            //Sniper: nagyon lassú, nagy sebzés/golyó
            Jatekos.Fegyverek.Add(new Fegyver(-1, -1, -1, golyoSebesseg, 50, "bullet", 55, "Sniper", 0));
            //LMG kontrollálhatatlan, összevissza lő, de nagyon sokat nagyon gyorsan
            Jatekos.Fegyverek.Add(new Fegyver(-1, 10, -1, golyoSebesseg, 4, "bullet", 95, "LMG", 0));
            //RPG: nagyon lassú, nagy sebzés/golyó, nagy golyó
            Jatekos.Fegyverek.Add(new Fegyver(20, 5, -1, golyoSebesseg, 200, "bullet", 50, "Cannon", 0));
            Jatekos.JelenFegyverID = 0;
            JatekAblak.eletbar.DataContext = Jatekos;
            JatekAblak.fegyvertext.DataContext = this;
            Jatekos.EletPont.MaxErtek = 100;
            Jatekos.EletPont.UjraTolt();


            

        }

        private void Gomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            bool elrejtkozepso = false;
            switch (label.Content.ToString())
            {
                case "Shotgun":
                    if (Jatekos.Penz >= 20)
                    {
                        Jatekos.Fegyverek[1].Loszer += 20;
                        Jatekos.Penz -= 20;
                    }
                    break;
                case "Sniper":
                    if (Jatekos.Penz >= 50)
                    {
                        Jatekos.Fegyverek[2].Loszer += 20;
                        Jatekos.Penz -= 50;
                    }
                    break;
                case "LMG":
                    if (Jatekos.Penz >= 200)
                    {
                        Jatekos.Fegyverek[3].Loszer += 100;
                        Jatekos.Penz -= 200;
                    }
                    break;
                case "Cannon":
                    if (Jatekos.Penz >= 20)
                    {
                        Jatekos.Fegyverek[4].Loszer += 5;
                        Jatekos.Penz -= 20;
                    }
                    break;
                case "Fire Rate":
                    if (JatekAblak.kozepPanel.Opacity > 0)
                    {
                        Jatekos.LovesiSebesseg += 0.05;
                        elrejtkozepso = true;
                    }
                    break;
                case "Movement Speed":
                    if (JatekAblak.kozepPanel.Opacity > 0)
                    {
                        Jatekos.Sebesség += 0.5;
                        elrejtkozepso = true;
                    }
                    break;
                case "Health Points":
                    if (JatekAblak.kozepPanel.Opacity > 0)
                    {
                        Jatekos.EletPont.MaxErtek += 20;
                        elrejtkozepso = true;
                    }
                    break;
                default:
                    break;
            }
            if (elrejtkozepso)
            {
                JatekAblak.kozepPanel.Opacity = 0;
            }
        }

        private void Builder()
        {
            if (WaveKezelo.lehetEpiteni && Jatekos.Penz >= 200)
            {
                Point hely = Mouse.GetPosition(JatekAblak.fővászon);
                int xcounter = 0 + Convert.ToInt32(Math.Floor(hely.X / 100));
                int ycounter = 0 + Convert.ToInt32(Math.Floor(hely.Y / 100));

                Vector gridhely = new Vector(xcounter, ycounter);
                Objektum pályaElem = new Objektum(gridhely.X * jelenPálya.GridMeret.X, gridhely.Y * JelenPálya.GridMeret.Y, jelenPálya.GridMeret.X, jelenPálya.GridMeret.Y, 200, 200, "Crate.png");

                Rect jatekteszt = new Rect(Jatekos.Hely.X, Jatekos.Hely.Y, Jatekos.Meret.X, Jatekos.Meret.Y);
                Rect barikadteszt = new Rect(pályaElem.Hely.X, pályaElem.Hely.Y, pályaElem.Meret.X, pályaElem.Meret.Y);
                if (!jatekteszt.IntersectsWith(barikadteszt))
                {
                    Jatekos.Penz -= 200;
                    JelenPálya.PályaElemek.Add(pályaElem);
                    JatekAblak.fővászon.Children.Add(pályaElem.Modell);
                }
            }
        }

        private void TalajHatterHozzaAd()
        {
            var HáttérMegkeres = from x in jelenPálya.PályaElemek
                                 where x.Modell.Tag != null && x.Modell.Tag.ToString() == "talaj"
                                 select x;
            foreach (var item in HáttérMegkeres)
            {
                Vector balfelsosarkaakepnek = new Vector(JatekAblak.ActualWidth / 2 - item.Modell.Width / 2, JatekAblak.ActualHeight / 2 - item.Modell.Height / 2);
                item.Hely = new Vector(balfelsosarkaakepnek.X, balfelsosarkaakepnek.Y);
                JatekAblak.fővászon.Children.Add(item.Modell);
            }
        }

        private void Gombok_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            double xValtozas = 0;
            double yValtozas = 0;
            foreach (Key key in Gombok)
            {
                switch (key)
                {
                    case Key.W:
                        yValtozas += 1;
                        break;
                    case Key.A:
                        xValtozas += 1;
                        break;
                    case Key.S:
                        yValtozas -= 1;
                        break;
                    case Key.D:
                        xValtozas -= 1;
                        break;
                    case Key.Space:
                        Builder();
                        break;
                    case Key.Q:
                        Jatekos.FegyverValtas(-1);
                        break;
                    case Key.E:
                        Jatekos.FegyverValtas(1);
                        break;
                    case Key.Escape:
                        jatekAblak.Close();
                        string kiir = "You got: " + WaveKezelo.pontSzamlalo.ToString() + " points.";
                        MessageBox.Show(kiir, "Result:", MessageBoxButton.OK);
                        break;
                    default:
                        break;
                }
            }
            if (xValtozas != 0 && yValtozas != 0)
            {
                xValtozas *= gyokKetto / 2;
                yValtozas *= gyokKetto / 2;
            }
            Jatekos.Irany = new Vector(xValtozas*Jatekos.Sebesség*-1, yValtozas*Jatekos.Sebesség*-1);
        }

        private void PályaLétrehoz(string palyanev,Jatekos jatekos)
        {
            Jatekos = jatekos;
            JelenPálya = new Pálya(palyanev, Jatekos);
            JatekAblak.fővászon.Children.Add(Jatekos.Modell);
            Jatekos.Hely = new Vector((JatekAblak.ActualWidth-Jatekos.Meret.X)/2,(JatekAblak.ActualHeight-Jatekos.Meret.Y)/2);

        }

        private void JatekAblakElindit()
        {
            JatekAblak = new JatekAblak();
            JatekAblak.KeyDown += JatekAblak_GombLe;
            JatekAblak.KeyUp += JatekAblak_GombFel;
            JatekAblak.MouseLeftButtonDown += JatekAblak_MouseLeftButtonDown;
            JatekAblak.WindowState = WindowState.Maximized;
            JatekAblak.Show();
        }

        private void JatekAblak_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GolyoHozzaAd();
        }

        private void GolyoHozzaAd()
        {
            if (loveslassito == 0 && WaveKezelo.lehetLoni)
            {
                Jatekos jatekosteszt = Jatekos;
                Point origo = new Point(jatekosteszt.Hely.X + jatekosteszt.Meret.X / 2, jatekosteszt.Hely.Y + jatekosteszt.Meret.Y / 2);

                Fegyver tempfegyver = Jatekos.Fegyverek[Jatekos.JelenFegyverID];
                if (tempfegyver.Loszer > 0 || tempfegyver.Loszer == -1)
                {
                    int golyolistameret = 1;
                    if (tempfegyver.Multishot != -1)
                    {
                        golyolistameret = Convert.ToInt32(tempfegyver.Multishot);
                    }
                    for (int i = 0; i < golyolistameret; i++)
                    {
                        LovesIrany = IranyKiszamol(Convert.ToInt32(Jatekos.Fegyverek[Jatekos.JelenFegyverID].Randomized));
                        LovesIrany = new Vector(LovesIrany.X * Jatekos.Fegyverek[Jatekos.JelenFegyverID].GolyoSebesseg, LovesIrany.Y * Jatekos.Fegyverek[Jatekos.JelenFegyverID].GolyoSebesseg);
                        Golyo golyo = GolyoTipusFegyverAlapjan(origo, tempfegyver);
                        if (tempfegyver.Specialsized != -1)
                        {
                            golyo.Meret = new Vector(tempfegyver.Specialsized, tempfegyver.Specialsized);
                        }
                        golyo.Irany = LovesIrany;
                        JelenPálya.PályaElemek.Add(golyo);
                        JatekAblak.fővászon.Children.Add(golyo.Modell);
                    }
                    if (tempfegyver.Loszer != -1)
                    {
                        Jatekos.Fegyverek[Jatekos.JelenFegyverID].Loszer -= 1;
                    }

                }
                double helper = (100 - Jatekos.Fegyverek[Jatekos.JelenFegyverID].LovesiSebesseg) * Jatekos.LovesiSebesseg;
                loveslassito = Convert.ToInt32(helper);
            }

        }

        private double AngleKiszamol()
        {
            Point origo = new Point(JatekAblak.ActualWidth/2, JatekAblak.ActualHeight/2);
            Point egerPoz = Mouse.GetPosition(jatekAblak);
            egerPoz.X -= origo.X;
            egerPoz.Y -= origo.Y;
            double test2 = Math.Atan2(egerPoz.Y, egerPoz.X);
            double angle2 = test2 * 180.00 / Math.PI;
            return angle2;
        }

        private Vector IranyKiszamol(int randommertek)
        {
            double angle2 = AngleKiszamol();
            if (randommertek > 0)
            {
                angle2 += random.Next(-1 * randommertek, randommertek);
            }
            double radofSzog = angle2 / 180.0 * Math.PI;
            double sineofAngle2 = Math.Sin(radofSzog);
            double cosineofAngle2 = Math.Cos(radofSzog);
            Vector bulletVector = new Vector(cosineofAngle2, sineofAngle2);
            return bulletVector;
        }

        private Golyo GolyoTipusFegyverAlapjan(Point origo,Fegyver fegyver)
        {
            
            return new Golyo(origo.X, origo.Y,fegyver.GolyoSebesseg,fegyver.Sebzes, fegyver.GolyoTextura);

            //lekéri a játékos jelenlegi fegyverét és az alapján létrehozza a golyót
        }

        private void JatekAblak_GombFel(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Gombok.Contains(e.Key))
            {
                Gombok.Remove(e.Key);
            }
        }

        private void JatekAblak_GombLe(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!Gombok.Contains(e.Key))
            {
                Gombok.Add(e.Key);
            }
        }

        private void IdozitoElindit()
        {
            Idozito = new DispatcherTimer();
            idozito.Interval = TimeSpan.FromMilliseconds(8);
            idozito.Tick += EgerEllenoriz;
            idozito.Tick += ElemekMozgas;
            idozito.Tick += ZombiSpawnol;
            idozito.Tick += JatekStatuszKezel;
            idozito.Start();
        }

        private void ZombiSpawnol(object sender, EventArgs e)
        {
            if (WaveKezelo.SpawnLassito == 0 && WaveKezelo.ZombiSzamlalo > 0)
            {
                Karakter tesztkar = new Karakter(0, 0, JelenPálya.GridMeret.X, JelenPálya.GridMeret.Y, 10*WaveKezelo.nehezsegSzorzo, 10*WaveKezelo.nehezsegSzorzo, 5*WaveKezelo.nehezsegSzorzo, 5*WaveKezelo.nehezsegSzorzo, "zombi.png");
                Point point = MIKezelo.KezdoPontMeghataroz(JelenPálya.Talaj, tesztkar.Modell);
                tesztkar.Hely = new Vector(point.X, point.Y);
                JelenPálya.PályaElemek.Add(tesztkar);
                JatekAblak.fővászon.Children.Add(tesztkar.Modell);
                WaveKezelo.ZombiSzamlalo--;
            }
            WaveKezelo.SpawnLassito--;
        }

        private void JatekStatuszKezel(object sender, EventArgs e)
        {
            if (Jatekos.EletPont.JelenErtek <= 0)
            {
                WaveKezelo.Statusz = JatekStatusz.Elvesztes;
            }
            Canvas.SetLeft(JatekAblak.mainstack, Jatekos.Hely.X - 200);
            Canvas.SetTop(JatekAblak.mainstack, Jatekos.Hely.Y - 200);
            if (WaveKezelo.Statusz == JatekStatusz.WaveKezd)
            {
                JatekAblak.mainstack.Opacity = 0;
                Panel.SetZIndex(JatekAblak.mainstack, -5);
                WaveKezelo.WaveKezd();
                WaveKezelo.Statusz = JatekStatusz.WaveMegy;
                HangKezelo.WaveKezd();
                Jatekos.EletPont.UjraTolt();
            }
            if (WaveKezelo.Statusz == JatekStatusz.WaveVege)
            {
                var zombik = from x in JelenPálya.PályaElemek
                             where x is Karakter && !(x is Jatekos) && !(x is Golyo)
                             select x;
                int i = 0;
                foreach (var item in zombik)
                {
                    i++;
                }
                if (i == 0)
                {
                    HangKezelo.kovetkezoStatusz = JatekStatusz.WaveVege;
                    WaveKezelo.WaveVege();
                    WaveKezelo.Statusz = JatekStatusz.Vasarlas;
                    Jatekos.Penz += Convert.ToInt32(WaveKezelo.szintSzamlalo * 500 * WaveKezelo.nehezsegSzorzo);
                    VasarlasMenu();
                }
            }
            if (WaveKezelo.Statusz == JatekStatusz.Elvesztes)
            {
                jatekAblak.Close();
                string kiir = "" + WaveKezelo.pontSzamlalo.ToString() + " pontot értél el.";
                MessageBox.Show(kiir, "Eredmény:", MessageBoxButton.OK);
            }
            //ha vásárlás vége akkor WaveKezelo.Statusz == JatekStatusz.WaveMegy
        }

        private void VasarlasMenu()
        {
            JatekAblak.mainstack.Opacity = 1;
            Panel.SetZIndex(JatekAblak.mainstack, 5);
            JatekAblak.kozepPanel.Opacity = 1;
        }

        private void ElemekMozgas(object sender, EventArgs e)
        {
            Vector jatekospozi = Jatekos.Hely;
            List<PályaElem> elemToTorol = JelenPálya.MozgatElemek();
            if (jatekospozi != Jatekos.Hely)
            {
                Thickness margin = JatekAblak.fővászon.Margin;
                JatekAblak.fővászon.Margin = new Thickness(margin.Left - Jatekos.Irany.X, margin.Top - Jatekos.Irany.Y, margin.Right + Jatekos.Irany.X, margin.Bottom + Jatekos.Irany.Y);

            }
            foreach (var item in elemToTorol)
            {
                PalyaElemTorol(item);
            }
        }

        private void EgerEllenoriz(object sender, EventArgs e)
        {
            LovesIrany = IranyKiszamol(Convert.ToInt32(Jatekos.Fegyverek[Jatekos.JelenFegyverID].Randomized));
            LovesIrany = new Vector(LovesIrany.X * Jatekos.Fegyverek[Jatekos.JelenFegyverID].GolyoSebesseg, LovesIrany.Y * Jatekos.Fegyverek[Jatekos.JelenFegyverID].GolyoSebesseg);
            double angle2 = AngleKiszamol();
            RotateTransform rotateTransform = new RotateTransform(angle2);
            Jatekos.Modell.LayoutTransform = rotateTransform;
            if (loveslassito != 0)
            {
                loveslassito -= 1;
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                GolyoHozzaAd();
            }
            
        }

        private void PalyaElemTorol(PályaElem item)
        {
            if (item is Objektum)
            {
                Objektum objektum = (Objektum)item;
                JelenPálya.PályaElemek.Remove(objektum);
                JatekAblak.fővászon.Children.Remove(objektum.Modell);
            }
            else if (item is Golyo)
            {
                Golyo golyo = (Golyo)item;
                JelenPálya.PályaElemek.Remove(golyo);
                JatekAblak.fővászon.Children.Remove(golyo.Modell);
            }
            else if (item is Karakter)
            {
                Karakter karakter = (Karakter)item;
                JelenPálya.PályaElemek.Remove(karakter);
                JatekAblak.fővászon.Children.Remove(karakter.Modell);
            }
            else if (item is PályaElem)
            {
                JelenPálya.PályaElemek.Remove(item);
                JatekAblak.fővászon.Children.Remove(item.Modell);
            }
            else
            {
                //semmi
            }
        }
    }

    class WaveKezelo
    {
        public Dictionary<Objektum,int> ObjektumokEsAraik { get; set; }
        public int valasztottObjektum { get; set; }
        private int spawnLassito;

        public int SpawnLassito
        {
            get { return spawnLassito; }
            set
            {
                if (value < 0)
                {
                    spawnLassito = SpawnLassitoBeallit();
                }
                else
                {
                    spawnLassito = value;
                }
            }
        }
        public bool lehetEpiteni { get; set; }
        public bool lehetLoni { get; set; }
        private int zombiSzamlalo;

        public int ZombiSzamlalo
        {
            get { return zombiSzamlalo; }
            set
            {
                if (value < zombiSzamlalo)
                {
                    PontNovekszik();
                }
                zombiSzamlalo = value; 
                if (value <= 0)
                {
                    Statusz = JatekStatusz.WaveVege;
                }
            }
        }
        public bool megAllitva { get; set; }

        public double nehezsegSzorzo { get; set; }//minden egyes nehézségnövekedéssel gyorsabbak, többet sebzők és erősebbek lesznek a zombik
        public int szintSzamlalo { get; set; }
        public double pontSzamlalo { get; set; }

        private JatekStatusz statusz;

        public JatekStatusz Statusz
        {
            get { return statusz; }
            set { statusz = value; }
        }


        public WaveKezelo()
        {
            megAllitva = false;
            lehetEpiteni = false;
            lehetLoni = true;
            zombiSzamlalo = 15;
            nehezsegSzorzo = 0.9;
            szintSzamlalo = 0;
            pontSzamlalo = 0;
            SpawnLassito = 20;
            Statusz = JatekStatusz.WaveKezd;
            valasztottObjektum = 0;
            ObjektumokEsAraik = new Dictionary<Objektum, int>();
            

        }

        public void WaveKezd()
        {
            nehezsegSzorzo += 0.1;
            zombiSzamlalo = Convert.ToInt32(Math.Floor(15 + (5 + szintSzamlalo) * nehezsegSzorzo));
            szintSzamlalo++;
            lehetLoni = true;
            lehetEpiteni = false;
        }
        

        public void PontNovekszik()
        {
            pontSzamlalo += 1 * (szintSzamlalo * 0.1) + 1 * nehezsegSzorzo;
        }
        private int SpawnLassitoBeallit()
        {
            return 20;
            //wave helyzet alapján állítja be
        }

        public void WaveVege()
        {
            lehetLoni = false;
            lehetEpiteni = true;
        }
    }

    internal class HangKezelo
    {
        private MediaPlayer zeneplayer;

        public MediaPlayer ZenePlayer
        {
            get { return zeneplayer; }
            set { zeneplayer = value; }
        }

        private MediaPlayer sfxplayer;

        public MediaPlayer SfxPlayer
        {
            get { return sfxplayer; }
            set { sfxplayer = value; }
        }
        private bool zenemegallitva;
        public JatekStatusz kovetkezoStatusz;
        public HangKezelo()
        {
            zeneplayer = new MediaPlayer();
            sfxplayer = new MediaPlayer();
            zenemegallitva = true;
            zeneplayer.MediaEnded += Zeneplayer_MediaEnded;
            zeneplayer.Volume = 5;

        }

        public void WaveKezd()
        {
            zeneplayer.Open(new Uri("Eroforrasok/Hangok/intro.wav", UriKind.Relative));
            zeneplayer.Play();
            kovetkezoStatusz = JatekStatusz.WaveMegy;
            zenemegallitva = false;
           
        }

        private void Zeneplayer_MediaEnded(object sender, EventArgs e)
        {
            switch (kovetkezoStatusz)
            {
                case JatekStatusz.WaveMegy:
                    zenemegallitva = false;
                    zeneplayer.Open(new Uri("Eroforrasok/Hangok/midloop.wav", UriKind.Relative));
                    zeneplayer.Position = TimeSpan.Zero;
                    zeneplayer.Play();
                    break;
                case JatekStatusz.WaveVege:
                    zenemegallitva = false;
                    zeneplayer.Open(new Uri("Eroforrasok/Hangok/outro.wav", UriKind.Relative));
                    zeneplayer.Position = TimeSpan.Zero;
                    zeneplayer.Play();
                    kovetkezoStatusz = JatekStatusz.Vasarlas;
                    break;
                case JatekStatusz.Vasarlas:
                    zenemegallitva = true;
                    zeneplayer.Stop();
                    break;
                case JatekStatusz.Elvesztes:
                    zenemegallitva = true;
                    zeneplayer.Stop();
                    break;
            }
        }
    }
}