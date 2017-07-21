using AutoMapper;

/// <summary>
/// This code is generated from NetGenerator
/// </summary>

namespace YourProjectName.BusinessLogic.Helpers
{
    public static class MapperEx
    {
        public static TTo CreateFrom<TFrom, TTo>(TFrom from)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
            var mapper = config.CreateMapper();
            return mapper.Map<TTo>(from);
        }

        public static void Map<TFrom, TTo>(TFrom from, ref TTo to)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
            var mapper = config.CreateMapper();
            mapper.Map(from, to);
        }
    }
}