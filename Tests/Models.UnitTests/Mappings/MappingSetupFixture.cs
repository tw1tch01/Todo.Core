using AutoMapper;
using Todo.Models.Mappings;
using NUnit.Framework;

namespace Todo.Models.UnitTests.Mappings
{
    [SetUpFixture]
    public class MappingSetUpFixture
    {
        protected readonly IMapper _mapper;

        public MappingSetUpFixture()
        {
            var configProvider = new MapperConfiguration(opt =>
            {
                opt.AddProfile<MappingProfile>();
            });
            _mapper = configProvider.CreateMapper();
        }
    }
}