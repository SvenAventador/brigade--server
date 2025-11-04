using Brigade.Domain.Services;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Brigade.Infrastructure.Services
{
    /// <summary>
    /// Реализация сервиса хеширования паролей с использованием алгоритма Argon2id.
    /// Обеспечивает криптографически безопасное хеширование и верификацию паролей.
    /// </summary>
    public class PasswordHasherService : IPasswordHasherService
    {
        /// <inheritdoc />
        public string HashPassword(string password)
            => PasswordHasherHelpers.HashPassword(password);

        /// <inheritdoc />
        public bool VerifyPassword(string password, string hashedPassword)
            => PasswordHasherHelpers.VerifyPassword(password, hashedPassword);
    }

    /// <summary>
    /// Вспомогательный класс для безопасного хеширования паролей с использованием Argon2id.
    /// Формат хеша: [salt (16 байт)][hash (32 байта)] в Base64-кодировке.
    /// </summary>
    internal static class PasswordHasherHelpers
    {
        private const int SaltSize = 16;      
        private const int HashSize = 32;      
        private const int DegreeOfParallelism = 8;
        private const int Iterations = 10;
        private const int MemorySize = 1024 * 1024; 

        /// <summary>
        /// Создаёт криптографически безопасный хеш пароля с генерацией случайной соли.
        /// Результат включает соль и хеш в формате Base64.
        /// </summary>
        /// <param name="password"> Пароль в виде открытого текста. </param>
        /// <returns> Base64-строка, содержащая соль и хеш. </returns>
        /// <exception cref="ArgumentException">
        /// Если <paramref name="password"/> равен <see langword="null"/> или пуст.
        /// </exception>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Пароль не может быть пустым.", nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            var hash = HashPassword(password, salt);

            var combinedBytes = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
            Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);

            return Convert.ToBase64String(combinedBytes);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                Iterations = Iterations,
                MemorySize = MemorySize
            };

            return argon2.GetBytes(HashSize);
        }

        /// <summary>
        /// Проверяет, соответствует ли указанный пароль сохранённому хешу.
        /// Извлекает соль из хеша и выполняет повторное хеширование с защитой от атак по времени.
        /// </summary>
        /// <param name="password"> Пароль в открытом виде. </param>
        /// <param name="hashedPassword"> 
        /// Хеш в формате Base64, сгенерированный методом <see cref="HashPassword"/>.ъ
        /// </param>
        /// <returns>
        /// <see langword="true"/>, если пароль корректен;
        /// иначе — <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Если любой из параметров равен <see langword="null"/> или пуст.
        /// </exception>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Пароль не может быть пустым.", nameof(password));

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("Хешированный пароль не может быть пустым.", nameof(hashedPassword));

            var combinedBytes = Convert.FromBase64String(hashedPassword);

            if (combinedBytes.Length != SaltSize + HashSize)
                return false; 

            var salt = new byte[SaltSize];
            var hash = new byte[HashSize];
            Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
            Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

            var newHash = HashPassword(password, salt);

            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }
    }
}