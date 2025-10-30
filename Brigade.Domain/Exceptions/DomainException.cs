namespace Brigade.Domain.Exceptions
{
    /// <summary>
    /// Представляет ошибки, возникающие во время операций, специфичных для домена.
    /// </summary>
    /// <remarks>
    /// DomainException служит базовым классом для исключений, указывающих на нарушения бизнес-правил 
    /// или модели предметной области. Наследуйте этот класс, чтобы создавать пользовательские исключения, 
    /// представляющие конкретные ошибки предметной области в логике вашего приложения.
    /// </remarks>
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) 
            : base(message) { }
    }
}