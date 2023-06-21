using PriceAggregator.Domain.Common;

namespace PriceAggregator.Domain.ValueObjects;

public class FinancialInstrument : ValueObject
{
    public string Symbol { get; }

    private FinancialInstrument() { }
    
    private FinancialInstrument(string symbol)
    {
        Symbol = symbol.ToLower();
    }

    public static FinancialInstrument From(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("The financial instrument symbol must be provided.", nameof(symbol));

        return new FinancialInstrument(symbol);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
    }

    public override int GetHashCode() => Symbol.GetHashCode();
    
    public static bool operator ==(FinancialInstrument instrument, string symbol)
    {
        return instrument.Symbol == symbol;
    }

    public static bool operator !=(FinancialInstrument instrument, string symbol)
    {
        return !(instrument == symbol);
    }
}