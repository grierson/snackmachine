namespace SnackMachine.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void AddMoneyTest()
    {
        Money money1 = new(1, 2, 3, 4, 5, 6);
        Money money2 = new(1, 2, 3, 4, 5, 6);

        Money sum = money1 + money2;

        Assert.Equal(2, sum.OneCentCount);
        Assert.Equal(4, sum.TenCentCount);
        Assert.Equal(6, sum.QuarterCentCount);
        Assert.Equal(8, sum.OneDollarCount);
        Assert.Equal(10, sum.FiveDollarCount);
        Assert.Equal(12, sum.TwentyDollarCount);
    }

    [Fact]
    public void StrutuarlEquality()
    {
        Money money1 = new(1, 2, 3, 4, 5, 6);
        Money money2 = new(1, 2, 3, 4, 5, 6);

        Assert.Equal(money1, money2);
        Assert.Equal(money1.GetHashCode(), money2.GetHashCode());
    }

    [Fact]
    public void MoneyNotEqual()
    {
        Money dollar = new(0, 0, 0, 1, 0, 0);
        Money hundredCent = new(100, 0, 0, 0, 0, 0);

        Assert.NotEqual(dollar, hundredCent);
        Assert.NotEqual(dollar.GetHashCode(), hundredCent.GetHashCode());
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, -1)]
    [InlineData(0, 0, 0, 0, -1, 0)]
    [InlineData(0, 0, 0, -1, 0, 0)]
    [InlineData(0, 0, -1, 0, 0, 0)]
    [InlineData(0, -1, 0, 0, 0, 0)]
    [InlineData(-1, 0, 0, 0, 0, 0)]
    public void NegativeMoney(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
        Action action = () => new Money(oneCentCount, tenCentCount, quarterCentCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

        Assert.Throws<InvalidOperationException>(action);
    }
}
