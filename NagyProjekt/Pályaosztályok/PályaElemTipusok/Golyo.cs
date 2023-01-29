using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NagyProjekt.Pályaosztályok.PályaElemTipusok
{
    class Golyo : Karakter
    {
        public Golyo(double xHely, double yHely, double sebesség, double sebzés, string textura) : base(xHely, yHely, 5, 5, -1, -1,sebesség,sebzés,textura)
        {
            //minden golyó élete végtelen, méretük pedig ugyanakkora
            //különleges lövedékek esetén a méretet lehet változtatni

        }
    }
}
