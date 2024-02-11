
namespace Plutoscarab.Bijection;

public partial struct Nat
{
    public readonly string ToJson()
    {
        var n = this;

        if (n < 3)
            return new[] { "null", "false", "true" }[(int)n];

        n -= 3;

        if (n.IsEven)
            return (n >> 1).ToJsonObject();

        n >>= 1;

        if (n.IsEven)
            return (n >> 1).ToJsonArray();

        n >>= 1;

        if (n.IsEven)
            return (n >> 1).ToString();

        n >>= 1;
        return n.ToJsonNumber();
    }

    private readonly string ToJsonNumber()
    {
        var (m, f, e) = this;
        return m.ToMantissa() + f.ToFraction() + e.ToExponent();
    }

    private readonly string ToMantissa() => ToSigned().ToString();

    private readonly string ToFraction() => IsZero ? "" : "." + new string(ToString().Reverse().ToArray());

    private readonly string ToExponent() => IsZero ? "" : "e" + ToSigned().ToString();

    private readonly string ToJsonString() => "\"" + string.Join("", ToWord(0x11000).Select(EncodeChar)) + "\"";

    private static string EncodeChar(int cp)
    {
        if (char.IsSurrogate((char)cp))
            return $"\\u{cp:X4}";

        var s = char.ConvertFromUtf32(cp);
        
        switch(s)
        {
            case "\"": return "\\\"";
            case "\\": return "\\\\";
            case "\b": return "\\b";
            case "\f": return "\\f";
            case "\n": return "\\n";
            case "\r": return "\\r";
            case "\t": return "\\t";
        }

        if (cp < 0x20)
            return $"\\u{cp:X4}";

        return s;
    }

    private readonly string ToJsonObject() => "{" + string.Join("; ", ToList(n => n.ToMember())) + "}";

    private readonly string ToMember()
    {
        var (n, v) = this;
        return n.ToJsonString() + ": " + v.ToJson();
    }

    private readonly string ToJsonArray() => "[" + string.Join(", ", ToList(n => n.ToJson())) + "]";
}