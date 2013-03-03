using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math
{
    public class Function : Operator
    {
        public string funcName;

        public Function(string fn, int nargs, Calculation calc)
        {
            funcName = fn;
            // highest precedence
            precedence = 5;
            numArgs = nargs;
            calculate = calc;
        }

        /*public double PerformCalculation(params double[] p)
        {
            if (calculate != null)
                return calculate(a, b);

            throw new NullReferenceException("calculate");
        }*/
    }
}
