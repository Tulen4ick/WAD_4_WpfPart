using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class Formula5 : Formula
    {
        public double Calc(double x, double z)
        {
            return Math.Sin(x) * Math.Cos(z);
        }
    }
}
