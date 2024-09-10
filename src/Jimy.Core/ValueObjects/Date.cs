using System.Globalization;
using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Date
{
    public DateTime Value { get; }

    public Date(DateTime value)
    {
        if (value == default)
        {
            throw new InvalidDateException("Date cannot be default.");
        }
        Value = value.Date;
    }
    
    public static Date UtcNow() => new Date(DateTime.UtcNow);

    public static Date Parse(string dateString)
    {
        if (DateTime.TryParse(dateString, new CultureInfo("en-US"), DateTimeStyles.None, out var result))
        {
            return new Date(result);
        }
        throw new InvalidDateException($"Cannot parse '{dateString}' to a valid Date.");
    }

    public static implicit operator DateTime(Date date) => date.Value;
    public static explicit operator Date(DateTime dateTime) => new Date(dateTime);

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}