namespace SnackMachine.Domain;

public sealed class Money : ValueObject<Money>
{
    public static readonly Money None = new(0, 0, 0, 0, 0, 0);
    public static readonly Money Cent = new(1, 0, 0, 0, 0, 0);
    public static readonly Money TenCent = new(0, 1, 0, 0, 0, 0);
    public static readonly Money QuarterCent = new(0, 0, 1, 0, 0, 0);
    public static readonly Money Dollar = new(0, 0, 0, 1, 0, 0);
    public static readonly Money FiveDollar = new(0, 0, 0, 0, 1, 0);
    public static readonly Money TwentyDollar = new(0, 0, 0, 0, 0, 1);

    public int OneCentCount { get; }
    public int TenCentCount { get; }
    public int QuarterCentCount { get; }
    public int OneDollarCount { get; }
    public int FiveDollarCount { get; }
    public int TwentyDollarCount { get; }

    public decimal Amount =>
        OneCentCount * 0.01m +
        TenCentCount * 0.10m +
        QuarterCentCount * 0.25m +
        OneDollarCount +
        FiveDollarCount * 5 +
        TwentyDollarCount * 20;

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
        return new Money(
            money1.OneCentCount + money2.OneCentCount,
            money1.TenCentCount + money2.TenCentCount,
            money1.QuarterCentCount + money2.QuarterCentCount,
            money1.OneDollarCount + money2.OneDollarCount,
            money1.FiveDollarCount + money2.FiveDollarCount,
            money1.TwentyDollarCount + money2.TwentyDollarCount);
    }

    public static Money operator *(Money money, int multiplier)
    {
        return new Money(
            money.OneCentCount * multiplier,
            money.TenCentCount * multiplier,
            money.QuarterCentCount * multiplier,
            money.OneDollarCount * multiplier,
            money.FiveDollarCount * multiplier,
            money.TwentyDollarCount * multiplier);
    }

    public static Money operator -(Money money1, Money money2)
    {
        return new Money(
            money1.OneCentCount - money2.OneCentCount,
            money1.TenCentCount - money2.TenCentCount,
            money1.QuarterCentCount - money2.QuarterCentCount,
            money1.OneDollarCount - money2.OneDollarCount,
            money1.FiveDollarCount - money2.FiveDollarCount,
            money1.TwentyDollarCount - money2.TwentyDollarCount);
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

    public Money Allocate(decimal moneyInTransaction)
    {
        int twentyDollarCount = Math.Min((int)(moneyInTransaction / 20), TwentyDollarCount);
        moneyInTransaction -= twentyDollarCount * 20;
        int fiveDollarCount = Math.Min((int)(moneyInTransaction / 5), FiveDollarCount);
        moneyInTransaction -= fiveDollarCount * 5;
        int oneDollarCount = Math.Min((int)moneyInTransaction, OneDollarCount);
        moneyInTransaction -= oneDollarCount;
        int quarterCentCount = Math.Min((int)(moneyInTransaction / 0.25m), QuarterCentCount);
        moneyInTransaction -= quarterCentCount * 0.25m;
        int tenCentCount = Math.Min((int)(moneyInTransaction / 0.10m), TenCentCount);
        moneyInTransaction -= tenCentCount * 0.10m;
        int oneCentCount = Math.Min((int)(moneyInTransaction / 0.01m), OneCentCount);
        moneyInTransaction -= oneCentCount * 0.01m;

        return new Money(
            oneCentCount,
            tenCentCount,
            quarterCentCount,
            oneDollarCount,
            fiveDollarCount,
            twentyDollarCount);
    }
}
