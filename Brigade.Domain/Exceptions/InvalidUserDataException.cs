namespace Brigade.Domain.Exceptions
{
    /// <summary>
    /// Некорректные данные пользователя.
    /// </summary>
    public class InvalidUserDataException : DomainException
    {
        public InvalidUserDataException(string message) 
            : base(message) { }
    }
}