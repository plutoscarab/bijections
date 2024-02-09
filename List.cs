
namespace Plutoscarab.Bijection;

public partial struct Nat
{
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
        return [.. n.ToTuple(length)];
    }

    public readonly List<T> ToList<T>(Func<Nat, T> selector, int dilution = DefaultDilution)
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
        return [.. n.ToTuple(length).Select(t => selector(t))];
    }
}