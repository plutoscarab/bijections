
using System.Numerics;

namespace Plutoscarab.Bijection;

public struct Nat :
    IEquatable<Nat>,
    IComparable<Nat>,
    IAdditiveIdentity<Nat, Nat>,
    IAdditionOperators<Nat, Nat, Nat>,
    IMultiplicativeIdentity<Nat, Nat>,
    IMultiplyOperators<Nat, Nat, Nat>,
    IComparisonOperators<Nat, Nat, bool>,
    IShiftOperators<Nat, int, Nat>,
    IBitwiseOperators<Nat, Nat, Nat>
{
    private static readonly Nat zero = new(0);

    private static readonly Nat one = new(1);

    private BigInteger b;

    public static Nat AdditiveIdentity => Zero;

    public static Nat MultiplicativeIdentity => One;

    public static Nat One => one;

    public static Nat Zero => zero;

    public static implicit operator BigInteger(Nat n) => n.b;

    public static explicit operator Nat(BigInteger n) => new(n);

    public static Nat operator +(Nat left, Nat right) => new(left.b + right.b);

    public static Nat operator -(Nat left, Nat right) => new(left.b - right.b);

    public static Nat operator *(Nat left, Nat right) => new(left.b * right.b);

    public static bool operator >(Nat left, Nat right) => left.b > right.b;

    public static bool operator >=(Nat left, Nat right) => left.b >= right.b;

    public static bool operator <(Nat left, Nat right) => left.b < right.b;

    public static bool operator <=(Nat left, Nat right) => left.b <= right.b;

    public static bool operator ==(Nat left, Nat right) => left.Equals(right);

    public static bool operator !=(Nat left, Nat right) => !(left == right);

    public static Nat operator <<(Nat value, int shiftAmount) => new(value.b << shiftAmount);

    public static Nat operator >>(Nat value, int shiftAmount) => new(value.b >> shiftAmount);

    public static Nat operator >>>(Nat value, int shiftAmount) => new(value.b >>> shiftAmount);

    public static Nat operator &(Nat left, Nat right) => new(left.b & right.b);

    public static Nat operator |(Nat left, Nat right) => new(left.b | right.b);

    public static Nat operator ^(Nat left, Nat right) => new(left.b ^ right.b);

    public static Nat operator ~(Nat value) => new(~value.b);

    public static Nat operator %(Nat left, Nat right) => new(left.b % right.b);

    public static Nat operator /(Nat left, Nat right) => new(left.b / right.b);

    public static Nat operator +(Nat value) => value;

    public static Nat operator ++(Nat value)
    {
        value.b++;
        return value;
    }

    public static Nat operator --(Nat value)
    {
        if (value.b.IsZero) throw new ArgumentOutOfRangeException(nameof(value));
        value.b--;
        return value;
    }

    public static IEnumerable<Nat> All()
    {
        var n = Zero;

        while (true)
        {
            yield return n;
            n++;
        }
    }

    public static IEnumerable<(Nat, Nat)> AllPairs() => All().Select(n => ((Nat, Nat))n);

    public static IEnumerable<(Nat, Nat, Nat)> AllTriples() => All().Select(n => ((Nat, Nat, Nat))n);

    public static IEnumerable<Nat[]> AllTuples(int length) => All().Select(n => n.ToTuple(length));

    public Nat(BigInteger n)
    {
        if (n.Sign < 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        b = n;
    }

    public Nat(Nat item1, Nat item2)
    {
        b = ((item1 + item2 + 1) * (item1 + item2) / 2 + item1).b;
    }

    public Nat((Nat, Nat) pair) : this(pair.Item1, pair.Item2)
    {
    }

    public readonly bool IsZero => b.IsZero;

    public readonly bool IsEven => b.IsEven;

    public readonly bool Equals(Nat other) => b.Equals(other.b);

    public override readonly bool Equals(object? obj) => obj is Nat n && Equals(n);

    public override readonly int GetHashCode() => b.GetHashCode();

    public override readonly string ToString() => b.ToString();

    public readonly int CompareTo(Nat other) => b.CompareTo(other.b);

    public readonly int GetByteCount() => b.GetByteCount();

    public static bool IsPow2(Nat value) => value.b.IsPowerOfTwo;

    public static Nat Log2(Nat value) => new(BigInteger.Log2(value.b));

    public static implicit operator (Nat, Nat)(Nat n)
    {
        if (n.b.IsZero) return (0, 0);
        var w = (Isqrt(8 * n + 1) - 1) / 2;
        var t = w * (w + 1) / 2;
        var y = n - t;
        return (y, w - y);
    }

    public static implicit operator (Nat, Nat, Nat)(Nat n)
    {
        var (a, b, c) = n;
        return (a, b, c);
    }

    public static implicit operator (Nat, Nat, Nat, Nat)(Nat n)
    {
        var (a, b, c, d) = n;
        return (a, b, c, d);
    }

    public static explicit operator Nat((Nat m, Nat n) pair) => new(pair.m, pair.n);

    public static implicit operator Nat(sbyte n) => new(n);

    public static implicit operator Nat(byte n) => new(n);

    public static implicit operator Nat(short n) => new(n);

    public static implicit operator Nat(ushort n) => new(n);

    public static implicit operator Nat(int n) => new(n);

    public static implicit operator Nat(uint n) => new(n);

    public static implicit operator Nat(long n) => new(n);

    public static implicit operator Nat(ulong n) => new(n);

    public static explicit operator sbyte(Nat n) => (sbyte)n.b;

    public static explicit operator byte(Nat n) => (byte)n.b;

    public static explicit operator short(Nat n) => (short)n.b;

    public static explicit operator ushort(Nat n) => (ushort)n.b;

    public static explicit operator int(Nat n) => (int)n.b;

    public static explicit operator uint(Nat n) => (uint)n.b;

    public static explicit operator long(Nat n) => (long)n.b;

    public static explicit operator ulong(Nat n) => (ulong)n.b;

    public static explicit operator float(Nat n) => (float)n.b;

    public static explicit operator double(Nat n) => (double)n.b;

    public static explicit operator decimal(Nat n) => (decimal)n.b;

    public static Nat Isqrt(Nat x)
    {
        if (x <= 1)
            return x;

        BigInteger q = 1, r = 0, t, v = x.b;

        while (q <= v)
            q <<= 2;

        while (q > 1)
        {
            q >>= 2;
            t = v - r - q;
            r >>= 1;
            if (t >= 0) { v = t; r += q; }
        }

        return new(r);
    }

    public readonly byte[] ToByteArray() => b.ToByteArray();

    public readonly void Deconstruct(out Nat p, out Nat q)
    {
        (p, q) = ((Nat, Nat))this;
    }

    public static (Nat, Nat) DivRem(Nat p, Nat q)
    {
        var (div, rem) = BigInteger.DivRem(p.b, q.b);
        return (new(div), new(rem));
    }

    public static (Nat, int) DivRem(Nat p, int q)
    {
        var (div, rem) = BigInteger.DivRem(p.b, q);
        return (new(div), (int)rem);
    }

    public Nat(byte[] array)
    {
        BigInteger b = new(array ?? throw new ArgumentNullException(nameof(array)));
        if (b.Sign < 0) throw new ArgumentOutOfRangeException(nameof(array));
        this.b = b;
    }

    public static Nat Min(Nat a, Nat b) => a < b ? a : b;

    public static Nat Binomial(Nat n, Nat k)
    {
        if (n < k) return Zero;
        if (k == 0) return One;
        k = Min(k, n - k);
        if (k == 1) return n;
        Nat i = 0, p = 1;

        while (true)
        {
            if (i >= k)
                return p;

            (i, p) = (i + 1, (n - i) * p / (i + 1));
        }
    }

    public Nat(Nat item1, Nat item2, Nat item3)
    {
        b = (Binomial(item1, 1) + Binomial(1 + item1 + item2, 2) + Binomial(2 + item1 + item2 + item3, 3)).b;
    }

    public Nat(Nat item1, Nat item2, Nat item3, Nat item4)
    {
        var i = item1;
        var sum = Binomial(i, 1);
        i += item2 + 1;
        sum += Binomial(i, 2);
        i += item3 + 1;
        sum += Binomial(i, 3);
        i += item4 + 1;
        sum += Binomial(i, 4);
        b = sum.b;
    }

    private static Nat UpperBinomial(Nat k, Nat n)
    {
        var to = k;

        while (Binomial(to, k) <= n + k)
        {
            to *= 2;
        }

        var from = to / 2;

        while (from != to)
        {
            var mid = (from + to) / 2;

            if (Binomial(mid, k) > n)
                to = mid;
            else
                from = mid + 1;
        }

        return from;
    }

    private static List<Nat> BinomialDigits(Nat k, Nat n)
    {
        List<Nat> list = [];

        while (!k.IsZero)
        {
            var m = UpperBinomial(k, n) - 1;
            list.Add(m);
            var bdigit = Binomial(m, k);
            k--;
            n -= bdigit;
        }

        return list;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2, out Nat item3)
    {
        var list = BinomialDigits(3, this);
        item1 = list[2];
        item2 = list[1] - list[2] - 1;
        item3 = list[0] - list[1] - 1;
    }

    public readonly void Deconstruct(out Nat item1, out Nat item2, out Nat item3, out Nat item4)
    {
        var list = BinomialDigits(4, this);
        item1 = list[3];
        item2 = list[2] - list[3] - 1;
        item3 = list[1] - list[2] - 1;
        item4 = list[0] - list[1] - 1;
    }

    public readonly Nat[] ToTuple(int length)
    {
        if (length < 1)
            throw new ArgumentOutOfRangeException(nameof(length));

        switch (length)
        {
            case 1:
                return [this];

            case 2:
                var (a, b) = this;
                return [a, b];
        }

        var list = BinomialDigits(length, this);
        var arr = new Nat[length];
        arr[0] = list[^1];

        for (var i = 1; i < length; i++)
        {
            arr[i] = list[length - 1 - i] - list[length - i] - 1;
        }

        return arr;
    }

    public static Nat FromTuple(Nat[] tuple)
    {
        if ((tuple ?? throw new ArgumentNullException(nameof(tuple))).Length < 1)
            throw new ArgumentException(null, nameof(tuple));

        switch (tuple.Length)
        {
            case 1:
                return tuple[0];

            case 2:
                return (Nat)(tuple[0], tuple[1]);
        }

        var t = tuple[0];
        var sum = Binomial(t, 1);

        for (var i = 1; i < tuple.Length; i++)
        {
            t += tuple[i] + 1;
            sum += Binomial(t, i + 1);
        }

        return sum;
    }

    public readonly BigInteger ToSigned() => IsEven ? b / 2 : -1 - b / 2;

    public static Nat FromSigned(BigInteger n) => new(n.IsEven ? n / 2 : -1 - n / 2);

    const int DefaultDilution = 10;

    public Nat(List<Nat> list, int dilution = DefaultDilution)
    {
        if (dilution < 1)
            throw new ArgumentOutOfRangeException(nameof(dilution));

        if (list.Count == 0)
        {
            b = 0;
            return;
        }

        // Create Nat is if list were tuple.
        var n = FromTuple([.. list]);

        // Get a tuple of fixed size.
        var t = n.ToTuple(dilution);

        // Append the length and get Nat from new tuple.
        n = 1 + FromTuple([..t, (list.Count - 1)]);
        b = n.b;
    }

    public readonly List<Nat> ToList(int dilution = DefaultDilution)
    {
        if (dilution < 1)
            throw new ArgumentOutOfRangeException(nameof(dilution));
            
        if (IsZero) return [];

        // Get diluted tuple.
        var t = (this - 1).ToTuple(dilution + 1);

        // Extract length.
        var length = 1 + (int)t[^1];

        // Get value from diluted tuple.
        var n = FromTuple(t[..^1]);

        // Create tuple of correct length.
        List<Nat> list = [.. n.ToTuple(length)];
        return list;
    }

    public static Nat FromWord(IEnumerable<Nat> symbols, int alphabetSize)
    {
        Nat n = 0;

        foreach (var d in symbols)
        {
            if (d >= alphabetSize)
                throw new ArgumentOutOfRangeException(nameof(symbols));

            var a = d + 1;

            if (a == alphabetSize)
            {
                a = 0;
                n++;
            }

            n = n * alphabetSize + a;
        }

        return n;
    }

    public readonly List<Nat> ToWord(int alphabetSize)
    {
        if (alphabetSize < 2)
            throw new ArgumentOutOfRangeException(nameof(alphabetSize));

        List<Nat> list = [];
        var n = this;

        while (n > 0)
        {
            var q = (n + (alphabetSize - 1)) / alphabetSize - 1;
            var a = n - q * alphabetSize;
            list.Add(a - 1);
            n = q;
        }

        list.Reverse();
        return list;
    }

    public static IEnumerable<List<Nat>> AllWords(int alphabetSize) =>
        All().Select(n => n.ToWord(alphabetSize));

    public static IEnumerable<BigInteger> AllIntegers() =>
        All().Select(n => (BigInteger)n);

    public static Nat FromIntList(IEnumerable<BigInteger> list, int dilution = DefaultDilution) =>
        new(list.Select(FromSigned).ToList(), dilution);

    public static List<BigInteger> ToIntList(Nat n, int dilution = DefaultDilution) =>
        n.ToList(dilution).Select(u => u.ToSigned()).ToList();

    public static IEnumerable<List<BigInteger>> AllIntLists(int dilution = DefaultDilution) =>
        All().Select(n => ToIntList(n, dilution));

    public static Nat FromRational(Nat p, Nat q)
    {
        if (q.IsZero) throw new ArgumentOutOfRangeException(nameof(q));

        List<byte> arr = [];
        var i = 0;
        var b = 1;
        var byt = 0;

        while (true)
        {
            var (div, rem) = Nat.DivRem(p, q);

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

        return new([.. arr]);
    }

    public (Nat, Nat) ToRational()
    {
        var last = 1;
        var k = 0;
        int a = 0, b = 1, c = 1, d = 0;

        foreach (var item in ToByteArray())
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

    public static IEnumerable<(Nat, Nat)> AllRationals() =>
        All().Select(n => n.ToRational());
}