namespace WC.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Считыватель строк файлов
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Итерационно возвращает строки файла
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        /// <returns> Итератор </returns>
        public IAsyncEnumerable<string> ReadAsync(string path);
    }
}