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