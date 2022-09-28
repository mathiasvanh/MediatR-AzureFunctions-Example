namespace MediatRExample.Common.Utils
{
    public static class StringExtensions
    {
        public static Guid ToGuidOrDefault(this string source, Guid defaultValue = default(Guid))
        {
            if (string.IsNullOrWhiteSpace(source))
                return defaultValue;
            
            Guid result;
            return Guid.TryParse(source, out result) ? result : defaultValue;
        }

        public static List<string> SplitToListOfString(this string source, char delimiter = ',')
        {
            return source?.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? new List<string>();
        }
        
        public static List<Guid> SplitToListOfGuid(this string source, char delimiter = ',')
        {
            var results = new List<Guid>();
            foreach (var guidString in source.SplitToListOfString(delimiter))
            {
                if (Guid.TryParse(guidString, out var result))
                {
                    results.Add(result);
                }
            }
            return results;
        }

        public static List<int> SplitToListOfInt(this string source, char delimiter = ',')
        {
            var results = new List<int>();
            foreach (var intString in source.SplitToListOfString(delimiter))
            {
                if (int.TryParse(intString, out var result))
                {
                    results.Add(result);
                }
            }
            return results;
        }
    }
}