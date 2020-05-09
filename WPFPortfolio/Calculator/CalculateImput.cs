using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPortfolio.Calculator
{
    class CalculateImput
    {
        public bool DivByZero { get; private set; }
        public double AddCalc(double secondNo, double firstNo)
        {
            return firstNo + secondNo;
        }

        public double MultiCalc(double secondNo, double firstNo)
        {
            return firstNo * secondNo;
        }

        public double SubstCalc(double secondNo, double firstNo)
        {
            return firstNo - secondNo;
        }

        public double DivCalc(double secondNo, double firstNo)
        {
            if (secondNo == 0)
            {
                DivByZero = true;
                return 0;
            }
            else
            {
                DivByZero = false;
                return firstNo / secondNo;
            }
        }
    }
}
