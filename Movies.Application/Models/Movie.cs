using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public partial class Movie
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required int YearOfRelease { get; init; }
        public string Slug => GenerateSlug();

        public required List<string> Genre { get; init; } = new();

        private string GenerateSlug()
        {
            
            var sluggedTitle = SlugRegex().Replace(Title, string.Empty)
                .ToLower()
                .Replace(" ", "-");
            return $"{sluggedTitle}-{YearOfRelease}";
        }

        /// <summary>
        /// Slugify the string by removing special characters and make it URL safe
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
        private static partial Regex SlugRegex();
    }
}
