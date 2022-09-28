using AutoMapper;
using MediatRExample.Common.Utils;

namespace MediatRExample.Common.Mapping.ValueConverters
{
    public class CsvToListOfGuidConverter : IValueConverter<string?, List<Guid>>
    {
        private readonly char _delimiter;

        public CsvToListOfGuidConverter(char delimiter = ',')
        {
            _delimiter = delimiter;
        }
        
        public List<Guid> Convert(string? source, ResolutionContext context)
        {
            return source?.SplitToListOfGuid(_delimiter) ?? new List<Guid>();
        }
    }
}