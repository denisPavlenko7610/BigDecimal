public struct BigDecimal
{
    public BigInteger Integer { get; set; }
    public BigInteger Scale { get; set; }

    public BigDecimal(BigInteger integer, BigInteger scale) : this()
    {
        Integer = integer;
        Scale = scale;
        while (Scale > 0 && Integer % 10 == 0)
        {
            Integer /= 10;
            Scale -= 1;
        }
    }

    public static implicit operator BigDecimal(decimal a)
    {
        BigInteger integer = (BigInteger)a;
        BigInteger scale = 0;
        decimal scaleFactor = 1m;
        while ((decimal)integer != a * scaleFactor)
        {
            scale += 1;
            scaleFactor *= 10;
            integer = (BigInteger)(a * scaleFactor);
        }
        return new BigDecimal(integer, scale);
    }

    public static BigDecimal operator *(BigDecimal a, BigDecimal b)
    {
        return new BigDecimal(a.Integer * b.Integer, a.Scale + b.Scale);
    }

    public override string ToString()
    {
        string s = Integer.ToString();
        if (Scale != 0)
        {
            if (Scale > Int32.MaxValue) return "[Undisplayable]";
            int decimalPos = s.Length - (int)Scale;
            s = s.Insert(decimalPos, decimalPos == 0 ? "0." : ".");
        }
        return s;
    }
}