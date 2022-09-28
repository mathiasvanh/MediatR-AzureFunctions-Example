using AutoMapper;

namespace MediatRExample.Common.Mapping
{
    public interface IMap<TSource, TDestination>
    {
        void AddMapping(Profile profile) => Map(profile.CreateMap<TSource, TDestination>());

        void Map(IMappingExpression<TSource, TDestination> map);
    }
}