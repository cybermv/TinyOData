namespace TinyOData.Extensions
{
    using System;

    /// <summary>
    /// String extensions that simplify the query parsing process
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Trims the string start and end and replaces all multiple spaces
        /// occurring in the given string  with a single space
        /// </summary>
        /// <param name="toClean">The string to clean up</param>
        /// <returns>The cleaned string</returns>
        public static string TrimInner(this string toClean)
        {
            if (toClean == null)
            {
                return null;
            }

            char[] chars = toClean.Trim().ToCharArray();

            if (chars.Length < 1)
            {
                return string.Empty;
            }

            char[] cleaned = new char[chars.Length];
            const char spaceChar = ' ';
            int newLength = 0;
            cleaned[newLength++] = chars[0];

            for (int idx = 1; idx < chars.Length; idx++)
            {
                if (cleaned[newLength - 1] == spaceChar && chars[idx] == spaceChar)
                {
                    continue;
                }
                cleaned[newLength++] = chars[idx];
            }

            return new string(cleaned, 0, newLength);
        }

        public static string[] Split(this string toSplit, string[] segments, StringSplitOptions options)
        {
            return new string[0];
        }
    }
}