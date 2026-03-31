using CalculatorNamespace;

public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        Assert.Equal(5, Calculator.Add(2, 3));
    }

    [Fact]
    public void Subtract_ReturnsCorrectDifference()
    {
        Assert.Equal(3, Calculator.Subtract(5, 2));
    }

    [Fact]
    public void Multiply_ReturnsCorrectProduct()
    {
        Assert.Equal(12, Calculator.Multiply(4, 3));
    }

    [Fact]
    public void Percentage_ReturnsCorrectValue()
    {
        Assert.Equal(20, Calculator.Percentage(200, 10));
    }

    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        Assert.Equal(5, Calculator.Divide(10, 2));
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Calculator.Divide(1, 0));
    }
}
