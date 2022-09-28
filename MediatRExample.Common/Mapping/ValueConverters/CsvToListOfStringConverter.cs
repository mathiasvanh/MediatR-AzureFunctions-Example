using AutoMapper;
using MediatRExample.Common.Utils;

namespace MediatRExample.Common.Mapping.ValueConverters
{
    public class CsvToListOfStringConverter : IValueConverter<string?, List<string>>
    {
        private readonly char _delimiter;

        public CsvToListOfStringConverter(char delimiter = ',')
        {
            _delimiter = delimiter;
        }
        
        public List<string> Convert(string? source, ResolutionContext context)
        {
            return source?.SplitToListOfString(_delimiter) ?? new List<string>();
        }
    }
}