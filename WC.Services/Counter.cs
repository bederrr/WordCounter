namespace WC.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Contracts;
    using Options = Contracts.Options;

    /// <inheritdoc />
    public class Counter : ICounter
    {
        private readonly IReader _reader;
        private readonly Options _options;

        /// <summary> ctor </summary>
        /// <param name="reader"> Считыватель </param>
        /// <param name="options"> Конфигурация </param>
        public Counter(IReader reader, IOptions<Options> options)
        {
            _reader = reader;
            _options = options.Value;

            _options.Dictionary = _options.IgnoreCase 
                ? _options.Dictionary.Select(x => x.ToLower()).Distinct().ToList() 
                : _options.Dictionary.Distinct().ToList();
        }

        public async Task<Report> Process()
        {
            var report = new Report(_options.Path, _options.IgnoreCase);

            if (File.Exists(_options.Path))
            {
                var fileReport = await ProcessFile(_options.Path);

                report.Files.Add(fileReport);

                return report;
            }

            if (!Directory.Exists(_options.Path))
                throw new Exception($"The path '{_options.Path}' does not exist");

            foreach (var file in Directory.GetFiles(_options.Path))
            {
                var fileReport = await ProcessFile(file);

                report.Files.Add(fileReport);
            }
            
            var matches = report.Files.SelectMany(x => x.Matches).ToList();
            report.Matches = CalculateMatches(matches);

            return report;
        }

        private Dictionary<string, int> CalculateMatches(IEnumerable<KeyValuePair<string, int>> matches)
        {
            var comparison = _options.IgnoreCase ? 
                StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var words = matches.Select(x => x.Key).Distinct().ToList();

            return words.Select(x => new
            { 
                Word = x, 
                Count = matches.Where(p => string.Equals(p.Key, x, comparison))
                    .Select(v => v.Value)
                    .Sum()
            })
                .ToDictionary(k => k.Word, v => v.Count);
        }

        private async Task<FileReport> ProcessFile(string path)
        {
            var report = new FileReport(Path.GetFileName(path));

            await foreach (var line in _reader.ReadAsync(path))
            {
                var words = line
                    .Split(_options.Separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                if (_options.IgnoreCase)
                    words = words.Select(x => x.ToLower()).ToList();

                var matches = words.GroupBy(x => x)
                    .Where(x => _options.Dictionary.Contains(x.Key))
                    .ToDictionary(k => k.Key, v => v.Count());

                foreach (var match in matches)
                {
                    if (report.Matches.TryGetValue(match.Key, out var count))
                        report.Matches[match.Key] = count  + match.Value;
                    else
                        report.Matches.Add(match.Key, match.Value);
                }
            }

            return report;
        }
    }
}