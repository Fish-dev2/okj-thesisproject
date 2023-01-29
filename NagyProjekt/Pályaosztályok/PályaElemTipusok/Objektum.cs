using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagyProjekt.Pályaosztályok
{
    class Objektum : PályaElem
    {
        //nem mozgó objektumok
        private bool golyoAtmegy;

        public bool GolyoAtmegy
        {
            get { return golyoAtmegy; }
            set { golyoAtmegy = value; }
        }

        public Objektum(double xHely, double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet,string textura) : base(xHely, yHely, xMéret, yMéret, maxElet, kezdoElet,textura)
        {
            golyoAtmegy = true;
        }
        public Objektum(double xHely, double yHely, double xMéret, double yMéret, double maxElet, double kezdoElet, string textura, bool golyoAtmegy) : base(xHely, yHely, xMéret, yMéret, maxElet, kezdoElet, textura)
        {
            this.golyoAtmegy = golyoAtmegy;
        }
    }
}
