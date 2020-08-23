using System.Collections.Generic;

namespace WC.Services
{
    using System;
    using System.Text;
    using Contracts;

    /// <summary>
    /// Принтер отчетов
    /// </summary>
    public class ReportPrinter
    {
        /// <summary> ctor </summary>
        public ReportPrinter() => Console.OutputEncoding = Encoding.UTF8;

        /// <summary>
        /// Выводит отчет 
        /// </summary>
        /// <param name="report"> Отчет </param>
        public void Print(Report report)
        {
            Console.WriteLine($"Path: {report.Path}");
            Console.WriteLine($"IgnoreCase: {report.IgnoreCase} \n");

            foreach (var match in report.Matches) 
                Print(match);

            Console.WriteLine(Environment.NewLine);

            report.Files.ForEach(Print);
        }

        private static void Print(FileReport report)
        {
            Console.WriteLine(report.Name);

            foreach (var match in report.Matches)
                Print(match);

            Console.WriteLine(Environment.NewLine);
        }

        private static void Print(KeyValuePair<string, int> match)
        {
            Console.WriteLine($"{match.Key} : {match.Value} times");
        }
    }
}