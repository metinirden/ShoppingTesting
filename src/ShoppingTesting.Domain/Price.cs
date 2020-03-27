using System;
using ShoppingTesting.Domain.Exception;
using ShoppingTesting.Domain.SharedKernel;

namespace ShoppingTesting.Domain
{
    /// <summary>
    /// Fiyat ValueObject'i, immutable olarak currency ve amount property'si ile instance'lar karşılaştırılmaktadır.
    /// ChangePrice, ChangeCurrency gibi metotlara sahip olmaması sebebi immutable olmasıdır.
    /// </summary>
    public class Price : ValueObject<Price>
    {
        private const int CURRENCY_LENGTH = 3;

        public string Currency { get; private set; }
        public double Amount { get; private set; }

        private Price()
        {
        }

        public Price(string currency, double amount)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != CURRENCY_LENGTH)
            {
                throw new DomainException($"{nameof(currency)} expected to be 3 chars!", new ArgumentException());
            }

            Currency = currency;

            if (!(amount > 0))
            {
                throw new DomainException($"{nameof(amount)} should be positive!", new ArgumentException());
            }

            Amount = amount;
        }

        protected override object[] PropertiesToCheckForEquality() => new object[] {Currency, Amount};
    }
}