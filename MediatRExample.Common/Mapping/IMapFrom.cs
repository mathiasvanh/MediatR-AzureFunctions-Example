using AutoMapper;

namespace MediatRExample.Common.Mapping
{
    public interface IMapFrom<TSource>
    {
        void AddMapping(Profile profile) => profile.CreateMap(typeof(TSource), GetType());
    }
}