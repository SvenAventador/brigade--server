namespace Brigade.Application.Common
{
    /// <summary>
    /// Представляет результат выполнения операции: успех или ошибка.
    /// </summary>
    /// <typeparam name="T">Тип успешного результата.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Указывает, успешно ли завершилась операция.
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Значение, возвращаемое при успешном выполнении.
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        /// Список ошибок, возникших при выполнении операции.
        /// </summary>
        public IReadOnlyList<string> Errors { get; private set; }

        private Result(bool isSuccess, 
                       T? value, 
                       IEnumerable<string>? errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = (errors?.ToList() ?? []).AsReadOnly();
        }

        /// <summary>
        /// Создаёт успешный результат.
        /// </summary>
        /// <param name="value"> Значение, возвращаемое при успехе. </param>
        /// <returns> Успешный результат. </returns>
        public static Result<T> Success(T value) 
            => new(true, value, null);

        /// <summary>
        /// Создаёт неуспешный результат с одной ошибкой.
        /// </summary>
        /// <param name="error"> Текст ошибки. </param>
        /// <returns> Неуспешный результат. </returns>
        public static Result<T> Failure(string error) 
            => new(false, default, [error]);

        /// <summary>
        /// Создаёт неуспешный результат со списком ошибок.
        /// </summary>
        /// <param name="errors"> Список текстов ошибок. </param>
        /// <returns> Неуспешный результат. </returns>
        public static Result<T> Failure(IEnumerable<string> errors) 
            => new(false, default, errors);

        /// <summary>
        /// Проверяет, является ли результат успешным.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, если операция завершилась успешно.
        /// </returns>
        public bool IsFailure => !IsSuccess;
    }

    /// <summary>
    /// Представляет тип, который не содержит данных.
    /// Аналог <see langword="System.Void" />, но может использоваться как обобщённый тип T в Result<T>.
    /// </summary>
    public readonly struct Unit
    {
        /// <summary>
        /// Получает экземпляр <see cref="Unit"/>.
        /// </summary>
        public static Unit Value { get; } = new(); 
    }
}