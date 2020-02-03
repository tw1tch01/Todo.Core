using AutoMapper;
using NUnit.Framework;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.UnitTests.Mappings
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