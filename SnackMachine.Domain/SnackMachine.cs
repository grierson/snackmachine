namespace SnackMachine.Domain;

public sealed class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } = Money.None;
    public Money MoneyInTransaction { get; private set; } = Money.None;

    public void InsertMoney(Money money)
    {
        Money[] demominations =
        {
            Money.Cent,
            Money.TenCent,
            Money.QuarterCent,
            Money.Dollar,
            Money.FiveDollar,
            Money.TwentyDollar
        };

        if (demominations.Contains(money) == false)
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = Money.None;
    }

    public void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = Money.None;
    }
}
