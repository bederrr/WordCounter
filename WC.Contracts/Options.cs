namespace WC.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Конфигурация
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Игнорирование регистра
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Словарь
        /// </summary>
        public List<string> Dictionary { get; set; }

        /// <summary>
        /// Разделители
        /// </summary>
        public List<string> Separators { get; set; }
    }
}