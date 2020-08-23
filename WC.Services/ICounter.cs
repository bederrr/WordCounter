namespace WC.Services
{
    using System.Threading.Tasks;
    using Contracts;
    
    /// <summary>
    /// Счетчик
    /// </summary>
    public interface ICounter
    {
        /// <summary>
        /// Считает слова. Возвращает отчет по считанным файлам
        /// </summary>
        /// <returns> Отчет </returns>
        Task<Report> Process();
    }
}