
using System.Diagnostics;
using System.Numerics;

namespace Plutoscarab.Bijection;

public class Program()
{
    public static void Main()
    {
        foreach (var w in Nat.AllWords(2).Take(30))
            Console.Write($"{string.Join("", w)}, ");

        Console.WriteLine();
        var str = "Hello, world!".ToArray().Select(c => (Nat)(int)c).ToList();

        for (var dilution = 2; dilution <= 12; dilution++)
        {
            var code = new Nat(str, dilution);
            var t = new string(code.ToList(dilution).Select(b => (char)(int)b).ToArray());
        }

        Random rand = new();

        for (Nat n = 0; n < 100_000; n++)
        {
            var (p, q) = n;
            Nat m = new(p, q);
            Debug.Assert(n == m);

            (p, q, var r) = n;
            m = new(p, q, r);
            Debug.Assert(n == m);

            (p, q, r, var s) = n;
            m = new(p, q, r, s);
            Debug.Assert(n == m);

            var k = 1 + rand.Next(20);
            var tuple = n.ToTuple(k);
            m = Nat.FromTuple(tuple);
            Debug.Assert(n == m);

            var list = n.ToList();
            m = new(list);
            Debug.Assert(n == m);

            (p, q) = n.ToRational();
            m = Nat.FromRational(p, q);
            Debug.Assert(n == m);

            var z = (BigInteger)n;
            m = (Nat)z;
            Debug.Assert(n == m);
        }

        static (string, double) ToTerm(Nat n)
        {
            var (m, f) = Nat.DivRem(n, 3);

            if (f == 0)
                return ((m + 1).ToString(), (double)m + 1);

            if (f == 1)
            {
                var (sm, dm) = ToTerm(m + 2);
                return ("√" + sm, Math.Sqrt(dm));
            }

            var (p, q) = m;
            var (sp, dp) = ToTerm(p);
            var (sq, dq) = ToTerm(q);
            return ($"({sp}+{sq})", dp + dq);
        }

        foreach (var (expr, value) in Nat.All().Take(1000).Select(ToTerm))
        {
            if (expr.Contains('√'))
                Console.WriteLine($"{expr} = {value}");
        }
    }
}