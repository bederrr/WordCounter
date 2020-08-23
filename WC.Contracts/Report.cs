namespace WC.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Отчет
    /// </summary>
    public class Report
    {
        /// <summary> ctor </summary>
        public Report(string path, bool ignoreCase)
        {
            Files = new List<FileReport>();
            Path = path;
            IgnoreCase = ignoreCase;
        }

        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Игнорирование регистра
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Список отчетов
        /// </summary>
        public List<FileReport> Files { get; set; }

        /// <summary>
        /// Все совпадения
        /// </summary>
        public Dictionary<string, int> Matches { get; set; }
    }
}