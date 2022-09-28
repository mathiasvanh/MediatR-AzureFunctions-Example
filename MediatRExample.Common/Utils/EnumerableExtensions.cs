using System.Text;

namespace MediatRExample.Common.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> source, int size)
        {
            T[]? bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)                
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                Array.Resize(ref bucket, count);
                yield return bucket.Select(x => x);
            }
        }

        // Return a list of strings in a parenthesis format like: ("item1","item2",...) 
        public static string ToParenthesisStringList(this IEnumerable<string> source, char separator = ',', char stringDelimiter = '"')
        {
            var result = new StringBuilder("(");

            foreach (var item in source)
            {
                if (result.Length > 1)
                {
                    result.Append(separator);
                }

                result.Append(stringDelimiter);
                result.Append(item);
                result.Append(stringDelimiter);
            }

            result.Append(')');

            return result.ToString();
        }
        
        // Return a list of integers in a parenthesis format like: (1,2,...)
        public static string ToParenthesisIntList(this IEnumerable<int> source, char separator = ',')
        {
            var result = new StringBuilder("(");

            foreach (var item in source)
            {
                if (result.Length > 1)
                {
                    result.Append(separator);
                }

                result.Append(item);
            }

            result.Append(')');

            return result.ToString();
        }
    }
}