using PriceAggregator.Domain.Common;

namespace PriceAggregator.Domain.ValueObjects;

public class FinancialInstrument : ValueObject
{
    public string Symbol { get; }
    
    private FinancialInstrument() { }

    public FinancialInstrument(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("The financial instrument symbol must be provided.", nameof(symbol));

        Symbol = symbol.ToLower();
    }
    
    public static FinancialInstrument White => new("#FFFFFF");

    public static FinancialInstrument Red => new("#FF5733");

    public static FinancialInstrument Orange => new("#FFC300");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
    }

    public override int GetHashCode() => Symbol.GetHashCode();
}