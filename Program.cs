
using System.Diagnostics;
using System.Numerics;

namespace Plutoscarab.Bijection;

public class Program()
{
    static string ToTerm(Nat n)
    {
        if (n < 3)
            return new[] {"x", "y", "\\pi"}[(int)n];

        var (m, f) = Nat.DivRem(n - 3, 5);

        if (f == 0)
            return (m + 1).ToString();

        if (--f == 0)
            return $"\\sqrt{{{ToTerm(m)}}}";
            
        var (p, q) = m;
        var sp = ToTerm(p);
        var sq = ToTerm(q);

        if (--f == 0)
            return $"\\frac{{{sp}}}{{{sq}}}";

        var op = "+- "[(int)--f];
        return $"\\left({sp}{op}{sq}\\right)";
    }

    public static void Main()
    {
        {
            Nat n = 0;
            Nat m = 1;

            for (var i = 0; i <= 20; i++)
            {
                var (a, b) = n;
                var (c, d, e) = n;
                var (p, q) = n.ToRational();
                var r = q == 1 ? p.ToString() : $"{p}/{q}";
                var list = n.ToList();
                var word = n.ToWord(10);
                Console.WriteLine($"|{n}|{n.ToSigned()}|({a}, {b})|({c}, {d}, {e})|{r}|[{string.Join(", ", list)}]|[{string.Join(", ", word)}]|");
                (n, m) = (m, n + m);
            }
        }

        {
            Nat n = 1;
            Nat m = 2;

            for (var i = 0; i <= 20; i++)
            {
                var (a, b) = n;
                var (pa, qa) = a.ToRational();
                var ra = qa == 1 ? pa.ToString() : $"{pa}/{qa}";
                var (pb, qb) = b.ToRational();
                var rb = qb == 1 ? pb.ToString() : $"{pb}/{qb}";
                var list = n.ToList();
                var ls = string.Join(", ", list.Select(k => {
                    var (p, q) = k;
                    return $"({p}, {q})";
                }));
                var hex = string.Join("", n.ToWord(26).Select(i => (char)((int)i + 65)));
                Console.WriteLine($"|{n}|({ra}, {rb})|[{ls}]|\"{hex}\"");
                (n, m) = (m, 2 * m + n);
            }
        }

        {
            Nat n = 1;
            Nat m = 2;

            for (var i = 0; i <= 20; i++)
            {
                var expr = ToTerm(n);
                Console.WriteLine($"|{n}|$${expr}$$|");
                (n, m) = (m, 2 * m + n);
            }
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
    }
}