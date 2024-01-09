namespace SnackMachine.Domain;

public sealed class Money : ValueObject<Money>
{
    public int OneCentCount { get; private set; }
    public int TenCentCount { get; private set; }
    public int QuarterCentCount { get; private set; }
    public int OneDollarCount { get; private set; }
    public int FiveDollarCount { get; private set; }
    public int TwentyDollarCount { get; private set; }

    public Money(
    int oneCentCount,
    int tenCentCount,
    int quarterCentCount,
    int oneDollarCount,
    int fiveDollarCount,
    int twentyDollarCount)
    {
        if (oneCentCount < 0)
            throw new InvalidOperationException();
        if (tenCentCount < 0)
            throw new InvalidOperationException();
        if (quarterCentCount < 0)
            throw new InvalidOperationException();
        if (oneDollarCount < 0)
            throw new InvalidOperationException();
        if (fiveDollarCount < 0)
            throw new InvalidOperationException();
        if (twentyDollarCount < 0)
            throw new InvalidOperationException();

        OneCentCount = oneCentCount;
        TenCentCount = tenCentCount;
        QuarterCentCount = quarterCentCount;
        OneDollarCount = oneDollarCount;
        FiveDollarCount = fiveDollarCount;
        TwentyDollarCount = twentyDollarCount;
    }

    public static Money operator +(Money money1, Money money2)
    {
        Money sum = new(
            money1.OneCentCount + money2.OneCentCount,
            money1.TenCentCount + money2.TenCentCount,
            money1.QuarterCentCount + money2.QuarterCentCount,
            money1.OneDollarCount + money2.OneDollarCount,
            money1.FiveDollarCount + money2.FiveDollarCount,
            money1.TwentyDollarCount + money2.TwentyDollarCount);

        return sum;
    }

    protected override bool EqualsCore(Money other)
    {
        return OneDollarCount == other.OneDollarCount &&
        FiveDollarCount == other.FiveDollarCount &&
        TwentyDollarCount == other.TwentyDollarCount &&
        QuarterCentCount == other.QuarterCentCount &&
        TenCentCount == other.TenCentCount &&
        OneCentCount == other.OneCentCount;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = OneDollarCount;
            hashCode = (hashCode * 397) ^ TenCentCount;
            hashCode = (hashCode * 397) ^ QuarterCentCount;
            hashCode = (hashCode * 397) ^ OneCentCount;
            hashCode = (hashCode * 397) ^ FiveDollarCount;
            hashCode = (hashCode * 397) ^ TwentyDollarCount;
            return hashCode;
        }
    }
}
