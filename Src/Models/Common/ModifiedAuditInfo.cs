using System;
using AutoMapper;
using Todo.Domain.Common;
using Todo.Models.Mappings;

namespace Todo.Models.Common
{
    public class ModifiedAuditInfo : IMaps<IModifiedAudit>
    {
        public string By { get; set; }
        public DateTime? On { get; set; }
        public string Process { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IModifiedAudit, ModifiedAuditInfo>()
                .ForMember(m => m.By, o => o.MapFrom(m => m.ModifiedBy))
                .ForMember(m => m.On, o => o.MapFrom(m => m.ModifiedOn))
                .ForMember(m => m.Process, o => o.MapFrom(m => m.ModifiedProcess));
        }
    }
}