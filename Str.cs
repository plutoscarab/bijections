
namespace Plutoscarab.Bijection;

public partial struct Nat
{
    public static Nat FromStr<T>(IEnumerable<T> str, T[] alphabet) where T : struct
    {
        var alphabetSize = alphabet.Length;
        var lkp = alphabet.Select((s, i) => (s, i)).ToDictionary(k => k.s, k => k.i);
        Nat n = 0;

        foreach (var d in str)
        {
            if (!lkp.TryGetValue(d, out int i))
                throw new ArgumentOutOfRangeException(nameof(str));

            var a = i + 1;

            if (a == alphabetSize)
            {
                a = 0;
                n++;
            }

            n = n * alphabetSize + a;
        }

        return n;
    }

    public readonly List<T> ToStr<T>(T[] alphabet)
    {
        var alphabetSize = alphabet.Length;

        if (alphabetSize < 2)
            throw new ArgumentOutOfRangeException(nameof(alphabetSize));

        List<T> str = [];
        var n = this;

        while (n > 0)
        {
            var q = (n + (alphabetSize - 1)) / alphabetSize - 1;
            var a = n - q * alphabetSize;
            str.Add(alphabet[(int)a - 1]);
            n = q;
        }

        str.Reverse();
        return str;
    }

    public static IEnumerable<List<T>> AllStrs<T>(T[] alphabet) =>
        All().Select(n => n.ToStr(alphabet));
}