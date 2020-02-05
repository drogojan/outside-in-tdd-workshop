using System.Linq;
using System;
using System.Collections.Generic;

namespace OpenChat.Application.Posts
{
    public class LanguageService : ILanguageService
    {
        private IEnumerable<string> innpropiateWords = new List<string> {
            "ice cream",
            "orange",
            "elephant"
        };

        public bool IsInappropriate(string text)
        {
            if (innpropiateWords.Any(word => text.Contains(word, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }
    }
}