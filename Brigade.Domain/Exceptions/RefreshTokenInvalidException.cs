namespace Brigade.Domain.Exceptions
{
    /// <summary>
    /// Некорректная работа токена.
    /// </summary>
    public class RefreshTokenInvalidException : DomainException
    {
        public RefreshTokenInvalidException(string message)
            : base(message) { }
    }
}