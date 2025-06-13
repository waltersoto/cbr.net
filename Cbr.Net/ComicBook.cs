using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Cbr.Net
{
    /// <summary>
    /// Represents a digital comic book container.
    /// </summary>
    public class ComicBook
    {
        private readonly List<string> _entries = new List<string>();

        /// <summary>
        /// Gets the format of the comic book container.
        /// </summary>
        public ComicBookFormat Format { get; private set; }

        /// <summary>
        /// Creates a <see cref="ComicBook"/> from the specified file.
        /// </summary>
        /// <param name="path">Path to the comic book archive.</param>
        /// <exception cref="NotSupportedException">Thrown when the format is not supported.</exception>
        public static ComicBook Load(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var format = GetFormatFromExtension(Path.GetExtension(path));
            var book = new ComicBook();
            switch (format)
            {
                case ComicBookFormat.Cbz:
                    book.LoadZip(path);
                    break;
                default:
                    // TODO: implement other formats using external libraries.
                    throw new NotSupportedException($"Format '{format}' is not yet supported.");
            }

            book.Format = format;
            return book;
        }

        /// <summary>
        /// Gets the list of entries (pages) contained in the archive.
        /// </summary>
        public IReadOnlyList<string> Entries => _entries;

        private void LoadZip(string path)
        {
            using var archive = ZipFile.OpenRead(path);
            foreach (var entry in archive.Entries)
            {
                if (!string.IsNullOrEmpty(entry.Name))
                {
                    _entries.Add(entry.FullName);
                }
            }
        }

        private static ComicBookFormat GetFormatFromExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                throw new ArgumentException("Invalid extension", nameof(extension));

            extension = extension.TrimStart('.').ToLowerInvariant();
            return extension switch
            {
                "cbr" => ComicBookFormat.Cbr,
                "cbz" => ComicBookFormat.Cbz,
                "cbt" => ComicBookFormat.Cbt,
                "cb7" => ComicBookFormat.Cb7,
                "cba" => ComicBookFormat.Cba,
                _ => throw new NotSupportedException($"Extension '{extension}' is not a supported comic book format.")
            };
        }
    }
}
