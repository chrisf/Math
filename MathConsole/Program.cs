using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Math;
using Math.Exceptions;

namespace MathConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = args[0];

            Calculator calc = new Calculator();
            double answer = calc.Solve(expression);
            Console.Out.WriteLine(answer);
        }
    }
}
