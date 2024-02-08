
using System.Diagnostics;
using System.Numerics;

public static class Bijective
{
    public static BigInteger FromPair(BigInteger m, BigInteger n) =>
        (m + n + 1) * (m + n) / 2 + n;

    public static (BigInteger, BigInteger) ToPair(BigInteger n)
    {
        var w = (Isqrt(8 * n + 1) / 2 - 1) / 2;
        var t = w * (w + 1) / 2;
        var y = n - t;
        return (w - y, y);
    }

    public static BigInteger Isqrt(BigInteger n)
    {
        if (n <= 1)
            return n;

        var x = n / 2;
        var next = BigInteger.Zero;

        while (next < x)
        {
            (x, next) = (next, (x + n / x) / 2);
        }

        return x;
    }

    public static BigInteger FromBaseK(IEnumerable<BigInteger> list, BigInteger k)
    {
        BigInteger n = 0;

        foreach (var d in list)
        {
            var a = d + 1;

            if (a == k)
            {
                a = 0;
                n++;
            }

            n = n * k + a;
        }

        return n;
    }

    public static List<BigInteger> ToBaseK(BigInteger n, BigInteger k)
    {
        if (k < 2)
            throw new ArgumentOutOfRangeException(nameof(k));

        List<BigInteger> list = [];

        while (n > 0)
        {
            var q = (n + k - 1) / k - 1;
            var a = n - q * k;
            list.Add(a - 1);
            n = q;
        }

        list.Reverse();
        return list;
    }

    public static BigInteger FromList(IEnumerable<BigInteger> list, int bias = 4)
    {
        if (!list.Any())
            return 0;

        List<BigInteger> items = [];
        var first = true;

        foreach (var item in list)
        {
            if (!first) items.Add(bias - 1);
            first = false;
            items.AddRange(ToBaseK(item, bias - 1));
        }

        return 1 + FromBaseK(items, bias);
    }

    public static List<BigInteger> ToList(BigInteger n, int bias = 4)
    {
        List<BigInteger> list = [];

        if (n <= 0)
            return list;

        List<BigInteger> items = [];
        var k = ToBaseK(n - 1, bias);

        foreach (var item in k)
        {
            if (item == bias - 1)
            {
                var value = FromBaseK(items, bias - 1);
                list.Add(value);
                items.Clear();
            }
            else
            {
                items.Add(item);
            }
        }

        var final = FromBaseK(items, bias - 1);
        list.Add(final);
        return list;
    }

    public static BigInteger FromRational(BigInteger p, BigInteger q)
    {
        if (p.Sign < 0 || q.Sign <= 0)
            throw new ArgumentOutOfRangeException();

        List<byte> arr = [];
        var i = 0;
        var b = 1;
        var byt = 0;

        while (true)
        {
            var (div, rem) = BigInteger.DivRem(p, q);

            for (var d = 0; d < div; d++)
            {
                byt |= b << i;
                ++i;

                if (i == 8)
                {
                    arr.Add((byte)byt);
                    byt = 0;
                    i = 0;
                }
            }

            b ^= 1;

            if (rem.IsZero) 
                break;

            (p, q) = (q, rem);
        }

        if (b == 1)
        {
            if (i == 0)
                arr[^1] |= 0x80;
            else
                byt |= b << (i - 1);
        }

        arr.Add((byte)byt);

        if (byt >= 0x80)
            arr.Add(0);

        return new(arr.ToArray());
    }

    public static (BigInteger, BigInteger) ToRational(BigInteger n)
    {
        var last = 1;
        var k = 0;
        int a = 0, b = 1, c = 1, d = 0;

        foreach (var item in n.ToByteArray())
        {
            for (var i = 0; i < 8; i++)
            {
                var p = (item >> i) & 1;

                if (p != last)
                {
                    (a, b) = (b, b * k + a);
                    (c, d) = (d, d * k + c);
                    last ^= 1;
                    k = 0;
                }

                ++k;
            }
        }

        if (last == 1)
        {
            (a, b) = (b, b * k + a);
            (c, d) = (d, d * k + c);
        }

        return (b, d);
    }

    public static BigInteger FromInteger(BigInteger n)
    {
        if (n.Sign >= 0)
            return 2 * n;

        return -1 - 2 * n;
    }

    public static BigInteger ToInteger(BigInteger n)
    {
        if (n.IsEven)
            return n / 2;

        return (n + 1) / -2;
    }
}

public class Program()
{
    public static void Main()
    {
        HashSet<BigInteger> seen1 = [];

        for (var n = 0; n < 100_000; n++)
        {
            var (p, q) = Bijective.ToPair(n);
            var m = Bijective.FromPair(p, q);
            Debug.Assert(n == m);

            var list = Bijective.ToList(n);
            m = Bijective.FromList(list);
            Debug.Assert(n == m);

            (p, q) = Bijective.ToRational(n);
            m = Bijective.FromRational(p, q);
            Debug.Assert(n == m);

            var z = Bijective.ToInteger(n);
            if (n < 20) Console.Write($"{z}, ");
            m = Bijective.FromInteger(z);
            Debug.Assert(n == m);
        }

        Console.WriteLine();

        var s = "Able was I ere I saw Elba.".ToArray().Select(c => (BigInteger)(int)c).ToList();
        var code = Bijective.FromList(s);
        var t = new string(Bijective.ToList(code).Select(b => (char)(int)b).ToArray());
        Console.WriteLine(t);
    }
}