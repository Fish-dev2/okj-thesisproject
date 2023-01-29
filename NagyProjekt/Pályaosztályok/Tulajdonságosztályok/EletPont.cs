using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagyProjekt
{
    public class EletPont : Bindable
    {
        //-1 eletpont azt jelenti, hogy halhatatlan
        private double jelenErtek;
        private double maxErtek;

        public double MaxErtek
        {
            get { return maxErtek; }
            set { maxErtek = value; TulajdonsagValtozott(); }
        }

        public double JelenErtek
        {
            get { return jelenErtek; }
            set { jelenErtek = value; TulajdonsagValtozott(); }
        }


        public EletPont(double kezdoErtek, double maxErtek)
        {
            this.jelenErtek = kezdoErtek;
            this.maxErtek = maxErtek;
            
        }

        public void Sebzodik(double sebzes)
        {
            if (jelenErtek != -1)
            {
                if (jelenErtek - sebzes <= 0)
                {
                    JelenErtek = 0;
                }
                else
                {
                    JelenErtek -= sebzes;
                }
            }
        }
        public void Gyogyul(double gyogyErtek)
        {
            if (jelenErtek != -1)
            {
                if (jelenErtek + gyogyErtek >= maxErtek)
                {
                    JelenErtek = maxErtek;
                }
                else
                {
                    JelenErtek += gyogyErtek;
                }
            }
        }
        public void UjraTolt()
        {
            if (jelenErtek != -1)
            {
                JelenErtek = maxErtek;
            }
        }
        public void Meghal()
        {
            if (jelenErtek != -1)
            {
                JelenErtek = 0;
            }
        }
    }
}
