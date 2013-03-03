using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math
{
    public class Operator
    {
        public delegate double Calculation(double[] args);
        protected Calculation calculate;

        public char symbol;
        public int precedence;
        public int numArgs;
        public Associativity assoc;

        public Operator() { }

        public Operator(char s, int p, Associativity a)
        {
            symbol = s;
            precedence = p;
            assoc = a;
        }

        public Operator(char s, int p, Associativity a, Calculation calc)
        {
            symbol = s;
            precedence = p;
            numArgs = 2;
            assoc = a;
            calculate = calc;
        }

        public double PerformCalculation(double[] args)
        {
            if (calculate != null)
                return calculate(args);

            throw new NotImplementedException();
        }

        public enum Associativity
        {
            Left,
            Right,
            None
        }
    }
}
