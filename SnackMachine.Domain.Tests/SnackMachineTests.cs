namespace SnackMachine.Domain.Tests;

public class SnackMachineTests
{
    [Fact]
    public void ReturnsMoney()
    {
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Dollar);

        snackMachine.ReturnMoney();

        Assert.Equal(0, snackMachine.MoneyInTransaction);
    }

    [Fact]
    public void InsertMoneyGoesToTransaction()
    {
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Cent);
        snackMachine.InsertMoney(Money.Dollar);

        Assert.Equal(1.01m, snackMachine.MoneyInTransaction);
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
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 1m));

        snackMachine.InsertMoney(Money.Dollar);

        snackMachine.BuySnack(1);

        Assert.Equal(0, snackMachine.MoneyInTransaction);
        Assert.Equal(Money.Dollar, snackMachine.MoneyInside);
        Assert.Equal(9, snackMachine.GetSnackPile(1).Quantity);
    }

    [Fact]
    public void CannotMakePurchaseWhenThereIsNoSnacks()
    {
        var snackMachine = new SnackMachine();

        Action action = () => snackMachine.BuySnack(1);

        Assert.Throws<InvalidOperationException>(action);
    }

    [Fact]
    public void CannotMakePurchaseWhenNotEnoughMoney()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 2m));
        snackMachine.InsertMoney(Money.Dollar);

        Action action = () => snackMachine.BuySnack(1);

        Assert.Throws<InvalidOperationException>(action);
    }

    [Fact]
    public void SnackMachineReturnsMoneyWithHighestDenominationFirst()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadMoney(Money.Dollar);

        snackMachine.InsertMoney(Money.QuarterCent);
        snackMachine.InsertMoney(Money.QuarterCent);
        snackMachine.InsertMoney(Money.QuarterCent);
        snackMachine.InsertMoney(Money.QuarterCent);

        snackMachine.ReturnMoney();

        Assert.Equal(4, snackMachine.MoneyInside.QuarterCentCount);
        Assert.Equal(0, snackMachine.MoneyInside.OneDollarCount);
    }

    [Fact]
    public void AfterPurchaseChangeIsReturned()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));
        snackMachine.LoadMoney(Money.TenCent * 10);

        snackMachine.InsertMoney(Money.Dollar);

        snackMachine.BuySnack(1);

        Assert.Equal(1.5m, snackMachine.MoneyInside.Amount);
        Assert.Equal(0, snackMachine.MoneyInTransaction);
    }

    [Fact]
    public void CannotBuySnackIfNotEnoughChange()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));

        snackMachine.InsertMoney(Money.Dollar);

        Action action = () => snackMachine.BuySnack(1);

        Assert.Throws<InvalidOperationException>(action);
    }
}
