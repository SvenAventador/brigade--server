namespace Brigade.Domain.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при попытке подтвердить email
    /// </summary>
    public class EmailAlreadyConfirmedException : DomainException
    {
        public EmailAlreadyConfirmedException(string message)
            : base(message) { }
    }
}