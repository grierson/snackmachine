namespace SnackMachine.Domain;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; } = Money.None;
    public decimal MoneyInTransaction { get; private set; } = 0;
    public List<Slot> Slots { get; private set; } = new List<Slot>{
        new Slot(1),
        new Slot(2),
        new Slot(3)
    };

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

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void ReturnMoney()
    {
        Money MoneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= MoneyToReturn;
        MoneyInTransaction = 0;
    }

    private Slot GetSlot(int position)
    {
        return Slots.Single(x => x.Position == position);
    }

    public void BuySnack(int position)
    {
        Slot slot = GetSlot(position);

        if (slot.SnackPile.Price > MoneyInTransaction)
            throw new InvalidOperationException();

        slot.SnackPile = slot.SnackPile.SubtractOne();

        Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

        if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
            throw new InvalidOperationException();

        MoneyInside -= change;

        MoneyInTransaction = 0;
    }

    public SnackPile GetSnackPile(int position)
    {
        var slot = Slots.Single(x => x.Position == position);
        return slot.SnackPile;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        var slot = Slots.Single(x => x.Position == position);
        slot.SnackPile = snackPile;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
