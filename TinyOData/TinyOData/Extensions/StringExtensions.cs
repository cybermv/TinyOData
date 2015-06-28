namespace TinyOData.Extensions
{
    /// <summary>
    /// String extensions that simplify the query parsing process
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces all multiple spaces with a single space
        /// </summary>
        /// <param name="toClean">The string to clean up</param>
        /// <returns>The cleaned string</returns>
        public static string TrimInner(this string toClean)
        {
            char[] chars = toClean.ToCharArray();
            char[] cleaned = new char[chars.Length];
            const char spaceChar = ' ';
            cleaned[0] = spaceChar;
            int cleanedUpLength = 1;

            for (int i = 0; i < chars.Length; i++)
            {
                if (cleaned[cleanedUpLength - 1] == spaceChar && chars[i] == spaceChar)
                {
                    continue;
                }
                cleaned[cleanedUpLength++] = chars[i];
            }
            return new string(cleaned, 0, cleanedUpLength).Trim();
        }
    }
}