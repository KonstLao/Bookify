namespace Bookify.Aplication.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public record UserResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string SecondName { get; private set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; private set; }
    }
}
