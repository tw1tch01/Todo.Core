using AutoMapper;

namespace Todo.Models.Mappings
{
    public interface IMaps<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}