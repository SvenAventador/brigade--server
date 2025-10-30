namespace Brigade.Domain.Exceptions
{
    /// <summary>
    /// Класс со статическими методами для проверки аргументов метода и генерации соответствующих исключений 
    /// в случае неудачной проверки.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Выдает исключение, если указанный аргумент равен null.
        /// </summary>
        /// <param name="argument"> Объект, проверяемый на недействительность. </param>
        /// <param name="argumentName"> Имя проверяемого аргумента. </param>
        /// <exception cref="ArgumentNullException"> Вызывается, если <paramref name="argument"/> имеет значение null. </exception>
        public static void AgainstNull(object? argument, string argumentName)
        {
            if (argument is null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Выдает исключение, если указанный строковый аргумент имеет значение null или пуст.
        /// </summary>
        /// <param name="argument"> Объект, проверяемый на недействительность. </param>
        /// <param name="argumentName"> Имя проверяемого аргумента. </param>
        /// <exception cref="ArgumentException">Вызывается, если <paramref name="argument"/> является нулем или пустой строкой.</exception>
        public static void AgainstNullOrEmpty(string? argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentException($"'{argumentName}' cannot be null or empty.", 
                                             argumentName);
        }

        /// <summary>
        /// Выдает исключение, если указанный строковый аргумент равен NULL, пуст или состоит только из пробелов.
        /// </summary>
        /// <param name="argument"> Объект, проверяемый на недействительность. </param>
        /// <param name="argumentName"> Имя проверяемого аргумента. </param>
        /// <exception cref="ArgumentException">Вызывается, если <paramref name="argument"/> является нулевым, пустым или состоит только из пробелов.</exception>
        public static void AgainstNullOrWhiteSpace(string? argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentException($"'{argumentName}' cannot be null or whitespace.", 
                                             argumentName);
        }

        /// <summary>
        /// Выдает исключение, если указанный целочисленный аргумент отрицательный.
        /// </summary>
        /// <param name="argument"> Объект, проверяемый на недействительность. </param>
        /// <param name="argumentName"> Имя проверяемого аргумента. </param>
        /// <exception cref="ArgumentOutOfRangeException">Вызывается, если <paramref name="argument"/> меньше нуля.</exception>
        public static void AgainstNegative(int argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, 
                                                      $"'{argumentName}' cannot be negative.");
        }

        /// <summary>
        /// Выдает исключение, если указанный вещественный аргумент отрицательный.
        /// </summary>
        /// <param name="argument"> Объект, проверяемый на недействительность. </param>
        /// <param name="argumentName"> Имя проверяемого аргумента. </param>
        /// <exception cref="ArgumentOutOfRangeException">Вызывается, если <paramref name="argument"/> меньше нуля.</exception>
        public static void AgainstNegative(double argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, 
                                                      $"'{argumentName}' cannot be negative.");
        }

        /// <summary>
        /// Выдает исключение, если указанный аргумент удовлетворяет предоставленному условию.
        /// </summary>
        /// <typeparam name="T"> Тип проверяемого аргумента. </typeparam>
        /// <param name="condition">
        /// Функция, определяющая условие проверки аргумента. Если функция возвращает 
        /// <see langword="true"/>, генерируется исключение. 
        /// </param>
        /// <param name="argument"> Аргумент для проверки на соответствие указанному условию. </param>
        /// <param name="argumentName">Имя проверяемого аргумента. Используется в исключении, если условие выполняется.</param>
        /// <param name="message">
        /// Необязательное пользовательское сообщение об ошибке для исключения. 
        /// Если значение равно null, используется сообщение по умолчанию.
        /// </param>
        /// <exception cref="ArgumentException">Вызывается, если <paramref name="condition"/> возвращает
        /// <see langword="true"/> для указанного <paramref name="argument"/>.</exception>
        public static void Against<T>(Func<T, bool> condition, 
                                      T argument, 
                                      string argumentName, 
                                      string message = null!)
        {
            if (condition(argument))
            {
                message ??= $"Argument '{argumentName}' failed the condition.";
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}