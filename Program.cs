using System;

namespace LabProject
{
 
    public class FractionalLinearFunction
    {
        protected const double ZeroThreshold = 1e-12;

        protected double[] _numerator = { 0.0, 0.0 };   // [a1, a0]
        protected double[] _denominator = { 0.0, 1.0 }; // [b1, b0]
 
        public FractionalLinearFunction() { }
 
        public FractionalLinearFunction(double a1, double a0, double b1, double b0)
        {
            SetCoefficients(a1, a0, b1, b0);
        }

        public void SetCoefficients(double a1, double a0, double b1, double b0) => (_numerator, _denominator) = (new[] { a1, a0 }, new[] { b1, b0 });
        public virtual void PrintCoefficients() => Console.WriteLine($"Дробово-лінійна функція:\n  Чисельник:  {_numerator[0]}*x + {_numerator[1]}\n  Знаменник:  {_denominator[0]}*x + {_denominator[1]}");
        public virtual double Evaluate(double x) => Math.Abs(_denominator[0] * x + _denominator[1]) < ZeroThreshold ? double.NaN : (_numerator[0] * x + _numerator[1]) / (_denominator[0] * x + _denominator[1]);
    }
 
    public class RationalFunction : FractionalLinearFunction
    {
        private double[] _numerator2 = { 0.0, 0.0, 0.0 };   // [a2, a1, a0]
        private double[] _denominator2 = { 0.0, 0.0, 1.0 }; // [b2, b1, b0]
 
        public RationalFunction() { }
 
        public RationalFunction(double a2, double a1, double a0, double b2, double b1, double b0)
        {
            SetCoefficients(a2, a1, a0, b2, b1, b0);
        }

        public void SetCoefficients(double a2, double a1, double a0, double b2, double b1, double b0) => (_numerator2, _denominator2) = (new[] { a2, a1, a0 }, new[] { b2, b1, b0 });
        public override void PrintCoefficients() => Console.WriteLine($"Дробова (раціональна) функція 2-го степеня:\n  Чисельник:  {_numerator2[0]}*x^2 + {_numerator2[1]}*x + {_numerator2[2]}\n  Знаменник:  {_denominator2[0]}*x^2 + {_denominator2[1]}*x + {_denominator2[2]}");
        public override double Evaluate(double x) => Math.Abs(_denominator2[0] * x * x + _denominator2[1] * x + _denominator2[2]) < ZeroThreshold ? double.NaN : (_numerator2[0] * x * x + _numerator2[1] * x + _numerator2[2]) / (_denominator2[0] * x * x + _denominator2[1] * x + _denominator2[2]);
    }

    class Program
    {
        static void Main()
        {
            var fl = new FractionalLinearFunction(2.0, 1.0, 3.0, -1.0);
            fl.PrintCoefficients();
            Console.WriteLine($"  Значення в точці x = 1: {fl.Evaluate(1.0)}\n");

            var rf = new RationalFunction(1.0, -2.0, 1.0, 1.0, 1.0, 0.0);
            rf.PrintCoefficients();
            Console.WriteLine($"  Значення в точці x = 1: {rf.Evaluate(1.0)}\n");

            FractionalLinearFunction poly = rf;
            poly.PrintCoefficients();
            Console.WriteLine($"  Значення (polymorphic) в точці x = 2: {poly.Evaluate(2.0)}\n");
        }
    }
}
