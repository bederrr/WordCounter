namespace WC.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Отчет файла
    /// </summary>
    public class FileReport
    {
        /// <summary> ctor </summary>
        public FileReport(string name)
        {
            Name = name;
            Matches = new Dictionary<string, int>();
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список совпадений
        /// </summary>
        public Dictionary<string, int> Matches { get; set; }
    }
}