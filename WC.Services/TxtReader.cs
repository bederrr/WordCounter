namespace WC.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <inheritdoc />
    public class TxtReader : IReader
    {
        public async IAsyncEnumerable<string> ReadAsync(string path)
        {
            if (!string.Equals(Path.GetExtension(path), ".txt", StringComparison.OrdinalIgnoreCase))
                yield return string.Empty;

            string line;
            using var sr = new StreamReader(path);
            while ((line = await sr.ReadLineAsync()) != null)
                yield return line;
        }
    }
}