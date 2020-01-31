using AutoMapper;
using Todo.DomainModels.Mappings;
using NUnit.Framework;

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