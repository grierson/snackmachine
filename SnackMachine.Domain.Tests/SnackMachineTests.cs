namespace SnackMachine.Domain.Tests;

public class SnackMachineTests
{
    [Fact]
    public void ReturnsMoney()
    {
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Dollar);

        snackMachine.ReturnMoney();

        Assert.Equal(0, snackMachine.MoneyInTransaction.Amount);
    }

    [Fact]
    public void InsertMoneyGoesToTransaction()
    {
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Cent);
        snackMachine.InsertMoney(Money.Dollar);

        Assert.Equal(1.01m, snackMachine.MoneyInTransaction.Amount);
    }

    [Fact]
    public void CanOnlyAcceptOneDenominationAtATime()
    {
        var snackMachine = new SnackMachine();

        var invalidCoin = Money.Cent + Money.Cent;

        Action action = () => snackMachine.InsertMoney(invalidCoin);

        Assert.Throws<InvalidOperationException>(action);
    }

    [Fact]
    public void MoneyInTransactionGoesToMoneyInsideAfterPurchase()
    {
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Dollar);
        snackMachine.InsertMoney(Money.Dollar);

        snackMachine.BuySnack();

        Assert.Equal(2m, snackMachine.MoneyInside.Amount);
        Assert.Equal(0m, snackMachine.MoneyInTransaction.Amount);
    }
}
