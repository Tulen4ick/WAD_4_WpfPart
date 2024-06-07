using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class Formula3: Formula
    {
        public double Calc(double x, double z)
        {
            return (x + z * z);
        }
    }
}
