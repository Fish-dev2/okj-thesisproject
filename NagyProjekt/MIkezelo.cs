using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NagyProjekt
{
    class MIkezelo
    {
        Random r;
        public MIkezelo() //külső osztály ami a zombik irányát, mozgását, tipusait, sebzését csinálja és ellenőrzi
        {
            r = new Random();
        }

        public Vector IrányVisszaad(Vector A, Vector B)
        {
            Vector ret = new Vector();
            ret = new Vector(B.X - A.X, B.Y - A.Y);

            double test2 = Math.Atan2(ret.Y, ret.X);
            double angle2 = test2 * 180.00 / Math.PI;
            double radofSzog = angle2 / 180.0 * Math.PI;
            double sineofAngle2 = Math.Sin(radofSzog);
            double cosineofAngle2 = Math.Cos(radofSzog);
            Vector bulletVector = new Vector(cosineofAngle2, sineofAngle2);

            //ABvektor = {Bx - Ax; By - Ay}

            return bulletVector;
        }

        public Point KezdoPontMeghataroz(Rectangle talaj,Rectangle karakter)
        {
            int spawnoldal = r.Next(0, 4); // 0= fent, 1 jobb, 2 lent, 3 bal
            int maxX = Convert.ToInt32(Canvas.GetLeft(talaj)) + Convert.ToInt32(talaj.Width) - Convert.ToInt32(karakter.Width);
            int maxY = Convert.ToInt32(Canvas.GetTop(talaj)) + Convert.ToInt32(talaj.Height) - Convert.ToInt32(karakter.Height);
            int randommagassag = r.Next(Convert.ToInt32(Canvas.GetTop(talaj)), maxY);
            int randomszelesseg = r.Next(Convert.ToInt32(Canvas.GetLeft(talaj)), maxX);

            switch (spawnoldal)
            {
                case 0:
                    return new Point(randomszelesseg, Canvas.GetTop(talaj));
                case 1:
                    return new Point(maxX, randommagassag);
                case 2:
                    return new Point(randomszelesseg, maxY);
                case 3:
                    return new Point(Canvas.GetLeft(talaj), randommagassag);
                default:
                    throw new Exception();//ez nem lehet
            }
        }
    }
}
