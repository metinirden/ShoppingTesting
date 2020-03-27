using System.Linq;

namespace ShoppingTesting.Domain.SharedKernel
{
    /// <summary>
    /// DDD ValueObject base class'ı. DDD'de value object'ler immutable'dır ve id'ye sahip değillerdir.
    /// Bu doğrultuda sahip olduğu değerler üstünden karşılaştırılması için implemente edilmesi gereken bir metod barındırmaktadır.
    /// </summary>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        protected abstract object[] PropertiesToCheckForEquality();

        protected bool Equals(ValueObject<T> other) =>
            PropertiesToCheckForEquality().SequenceEqual(other.PropertiesToCheckForEquality());

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((ValueObject<T>) obj);
        }

        public override int GetHashCode() =>
            PropertiesToCheckForEquality()
                .Aggregate(7,
                    (current, prop) => current * (prop.GetHashCode() + 13)
                );
    }
}