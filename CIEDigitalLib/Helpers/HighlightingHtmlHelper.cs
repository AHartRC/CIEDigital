using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIEDigitalLib.Helpers
{
    public static class HighlightingHtmlHelper
    {
        private const string HighlighStart = "<span class=\"bg-primary\">";

        private const string HighlighEnd = "</span>";

        public static IHtmlString HighlightKeyWords(this string term, string keyword)
        {
            return term.HighlightKeyWords(new[] {keyword});
        }

        public static IHtmlString HighlightKeyWords(this string term, IEnumerable<string> keywords)
        {
            if (keywords == null)
            {
                throw new ArgumentNullException("keywords");
            }

            if (term == null)
            {
                return MvcHtmlString.Empty;
            }

            // Collect all characters that must be highlighted
            var highlightCharIndices = new bool[term.Length];

            foreach (var keyword in keywords.Where(k => !string.IsNullOrEmpty(k)).OrderByDescending(k => k.Length))
            {
                var currentIndex = 0;

                while (currentIndex != -1 && currentIndex < term.Length)
                {
                    currentIndex = term.IndexOf(keyword, currentIndex, StringComparison.CurrentCultureIgnoreCase);

                    if (currentIndex > -1)
                    {
                        for (var i = 0; i < keyword.Length; i++)
                        {
                            highlightCharIndices[currentIndex + i] = true;
                        }

                        currentIndex += keyword.Length;
                    }
                }
            }

            // Highlight all necessary characters
            var offset = 0;

            for (var i = 0; i < highlightCharIndices.Length; i++)
            {
                if (!highlightCharIndices[i])
                {
                    continue;
                }

                var start = i;
                var end = i + 1;
                var current = i;

                while (current < highlightCharIndices.Length - 1 && highlightCharIndices[++current])
                {
                    end++;
                }

                term = term.Insert(offset + end, HighlighEnd);
                term = term.Insert(offset + start, HighlighStart);

                offset += HighlighStart.Length + HighlighEnd.Length;
                i += end - start;
            }

            return MvcHtmlString.Create(term);
        }
    }
}