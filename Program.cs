using System;
// Батьківський клас: дробово-лінійна функція
class FractionalLinearFunction
{
    protected double[] numerator = { 0.0, 0.0 };   // [a1, a0]
    protected double[] denominator = { 0.0, 1.0 }; // [b1, b0]
    public void SetCoefficients(double a1, double a0, double b1, double b0) => (numerator, denominator) = (new[] { a1, a0 }, new[] { b1, b0 });
    public virtual void PrintCoefficients() => Console.WriteLine($"Дробово-лінійна функція:\n  Чисельник:  {numerator[0]}*x + {numerator[1]}\n  Знаменник:  {denominator[0]}*x + {denominator[1]}");
    /// <summary>
    /// Evaluate a polynomial given coefficients in descending order (Horner's method): coeffs[0]*x^n + ... + coeffs[n].
    /// Protected so derived classes can reuse the implementation.
    /// </summary>
    protected static double EvaluatePolynomial(double[] coeffs, double x)
    {
        double result = 0.0;
        foreach (var c in coeffs)
        {
            result = result * x + c;
        }
        return result;
    }

    public virtual double Evaluate(double x)
    {
        var denom = EvaluatePolynomial(denominator, x);
        if (Math.Abs(denom) < 1e-12) return double.NaN;
        var num = EvaluatePolynomial(numerator, x);
        return num / denom;
    }
}
// Похідний клас: дробова функція другого степеня
class RationalFunction : FractionalLinearFunction
{
    private double[] numerator2 = { 0.0, 0.0, 0.0 };   // [a2, a1, a0]
    private double[] denominator2 = { 0.0, 0.0, 1.0 }; // [b2, b1, b0]
    public void SetCoefficients(double a2, double a1, double a0, double b2, double b1, double b0) => (numerator2, denominator2) = (new[] { a2, a1, a0 }, new[] { b2, b1, b0 });
    public override void PrintCoefficients() => Console.WriteLine($"Дробова (раціональна) функція 2-го степеня:\n  Чисельник:  {numerator2[0]}*x^2 + {numerator2[1]}*x + {numerator2[2]}\n  Знаменник:  {denominator2[0]}*x^2 + {denominator2[1]}*x + {denominator2[2]}");
    public override double Evaluate(double x)
    {
        var denom = EvaluatePolynomial(denominator2, x);
        if (Math.Abs(denom) < 1e-12) return double.NaN;
        var num = EvaluatePolynomial(numerator2, x);
        return num / denom;
    }
}
// Тестовий запуск
class Program
{
    static void Main()
    {
        var fl = new FractionalLinearFunction();
        fl.SetCoefficients(2.0, 1.0, 3.0, -1.0);
        fl.PrintCoefficients();
        Console.WriteLine($"  Значення в точці x = 1: {fl.Evaluate(1.0)}\n");
        var rf = new RationalFunction();
        rf.SetCoefficients(1.0, -2.0, 1.0, 1.0, 1.0, 0.0);
        rf.PrintCoefficients();
        Console.WriteLine($"  Значення в точці x = 1: {rf.Evaluate(1.0)}\n");
        FractionalLinearFunction poly = rf;
        poly.PrintCoefficients();
        Console.WriteLine($"  Значення (polymorphic) в точці x = 2: {poly.Evaluate(2.0)}\n");
    }
}
