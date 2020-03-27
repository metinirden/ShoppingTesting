namespace ShoppingTesting.Domain.Exception
{
    /// <summary>
    /// Domain katmanında fırlatılan exception, üst katmanlardan ayrışmayı kolaylaştırmaktadır.
    /// InnerException olarak detay barındırmaktadır.
    /// </summary>
    public class DomainException : System.Exception
    {
        public DomainException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}