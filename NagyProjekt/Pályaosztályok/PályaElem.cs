using System;
using System.Collections.Generic;
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
    public class PályaElem : Bindable
    {
        //bármilyen pályaelem alapvető tulajdonságait határozza meg:
        //Életpont, kezdőpozi, currentpozi,mozgás,stb.

        //XMéret, YMéret: a pályaelemek effektive egy gridként fognak elhelyezkedni, és ebben ez az oszlop és sorméretet határozza meg, általában ez 1.
        //textura string: relativ lokációja az adott objektum textúrájának
        //XHely,YHely: a griden lévő helye az objektumnak
        //Életpont: adott tárgy életpontja, -1 érték esetén végtelen.
        //A pályamérettől függően fogja beállítani a karakterek/objektumok helyét és méretét a játék,
        //így itt a gridben lévő helyüket lehet megadni, ez lehet egész szám, vagy tizedessel ellátott is, 
        //de így nem lesznek túllógó, vagy túlméretezett objektumok.

        private Vector meret;

        public Vector Meret
        {
            get { return meret; }
            set { meret = value; TulajdonsagValtozott(); }
        }

        private Vector hely;

        public Vector Hely
        {
            get { return hely; }
            set { hely = value; TulajdonsagValtozott(); }
        }

        private EletPont eletPont;

        public EletPont EletPont
        {
            get { return eletPont; }
            set { eletPont = value; TulajdonsagValtozott(); }
        }

        private Rectangle modell;

        public Rectangle Modell
        {
            get { return modell; }
            set { modell = value; TulajdonsagValtozott(); }
        }

        //textúra implementálása

        public PályaElem(double xHely, double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet, string textura)
        {
            Meret = new Vector(xMéret, yMéret);
            Hely = new Vector(xHely, yHely);
            EletPont = new EletPont(kezdoElet, maxElet);
            Modell = new Rectangle();
            if (textura == "bullet")
            {
                Modell.Fill = new SolidColorBrush(Colors.Gray);
            }
            else if (textura == "background.png")
            {
                Modell.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Eroforrasok/Texturak/" + textura)));
            }
            else
            {
                Modell.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Eroforrasok/Texturak/Karakterek/" + textura)));
            }
            

            AdatkotMindenAdat();

        }

        private void AdatkotMindenAdat()
        {
            Binding myBind = new Binding("Hely.X");
            myBind.Source = this;
            Modell.SetBinding(Canvas.LeftProperty, myBind);

            myBind = new Binding("Hely.Y");
            myBind.Source = this;
            Modell.SetBinding(Canvas.TopProperty, myBind);

            myBind = new Binding("Meret.X");
            myBind.Source = this;
            Modell.SetBinding(FrameworkElement.WidthProperty, myBind);

            myBind = new Binding("Meret.Y");
            myBind.Source = this;
            Modell.SetBinding(FrameworkElement.HeightProperty, myBind);
            Panel.SetZIndex(Modell, 3);
        }
    }
}