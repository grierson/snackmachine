namespace SnackMachine.Domain;

public sealed class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; } = new Money(0, 0, 0, 0, 0, 0);
    public Money MoneyInTransaction { get; private set; } = new Money(0, 0, 0, 0, 0, 0);

    public void InsertMoney(Money money)
    {
        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = new Money(0, 0, 0, 0, 0, 0);
    }

    public void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
    }
}
