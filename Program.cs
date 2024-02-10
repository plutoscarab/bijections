﻿
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Plutoscarab.Bijection;

public class Program()
{
    static string ToTerm(Nat n)
    {
        if (n < 3)
            return new[] { "x", "y", "\\pi" }[(int)n];

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

        var op = "+-"[(int)--f];
        return $"\\left({sp}{op}{sq}\\right)";
    }

    public static string ToTree(Nat n)
    {
        var (m, b) = Nat.DivRem(n, 2);

        if (b == 0)
            return "leaf " + m.ToString();

        var (left, right) = m;
        return "tree (" + ToTree(left) + ") (" + ToTree(right) + ")";
    }

    public static string PolyString(List<BigInteger> coeffs)
    {
        if (coeffs is null)
        {
            return "0";
        }

        var s = new StringBuilder();

        for (var power = 0; power < coeffs.Count; power++)
        {
            var coeff = coeffs[power];

            if (coeff != 0)
            {
                if (power == 0)
                {
                    s.Append(coeff);
                }
                else
                {
                    if (coeff < 0)
                    {
                        s.Append('-');
                    }
                    else if (s.Length > 0)
                    {
                        s.Append('+');
                    }

                    var abs = BigInteger.Abs(coeff);

                    if (abs != 1)
                    {
                        s.Append(abs);
                    }

                    s.Append('n');

                    if (power > 1)
                    {
                        s.Append('^');
                        if (power < 10) s.Append(power);
                        else s.Append("{" + power + "}");
                    }
                }
            }
        }

        return s.ToString();
    }

    public static void Main()
    {
        {
            Nat n = 0;
            Nat m = 1;
            var hiero = Enumerable.Range(0x13000, 128).Select(char.ConvertFromUtf32).ToArray();

            for (var i = 0; i <= 20; i++)
            {
                var (a, b) = n;
                var (c, d, e) = n;
                var (p, q) = n.ToRational();
                var r = q == 1 ? p.ToString() : $"{p}/{q}";
                var list = n.ToList();
                var set = n.ToSet();
                var word = n.ToStr(hiero);
                Console.WriteLine($"|{n}|{n.ToSigned()}|({a},{b})|({c},{d},{e})|{r}|[{string.Join(",", list)}]|[{string.Join(",", set)}]|\"{string.Join("", word)}\"|");
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
                var list = n.ToList(k =>
                {
                    var (p, q) = k;
                    return $"({p}, {q})";
                });
                var ls = string.Join(", ", list);
                var ncoeff = n.ToList(4);
                ncoeff[^1]++;
                var coeff = ncoeff.Select(n => -n.ToSigned()).ToList();
                Console.WriteLine($"|{n}|({ra}, {rb})|[{ls}]|$${PolyString(coeff)}$$|");
                (n, m) = (m, 2 * m + n);
            }
        }

        {
            Nat n = 1;
            Nat m = 4;

            for (var i = 0; i <= 20; i++)
            {
                var expr = ToTerm(n);
                Console.WriteLine($"|{n}|$${expr}$$|`{ToTree(n)}`|");
                (n, m) = (m, 5 * m + 3 * n);
            }
        }

        Random rand = new();

        for (Nat n = 0; n < 100_000; n++)
        {
            var (p, q) = n;
            Nat m = new((p, q));
            Debug.Assert(n == m);

            (p, q, var r) = n;
            m = new((p, q, r));
            Debug.Assert(n == m);

            (p, q, r, var s) = n;
            m = new((p, q, r, s));
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

            var z = n.ToSigned();
            m = Nat.FromSigned(z);
            Debug.Assert(n == m);

            char[] alphabet = ['a', 'b', 'c'];
            var str = n.ToStr(alphabet);
            m = Nat.FromStr(str, alphabet);
            Debug.Assert(n == m);
        }
    }
}