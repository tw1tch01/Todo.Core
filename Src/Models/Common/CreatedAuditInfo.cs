﻿using System;
using AutoMapper;
using Todo.Domain.Common;
using Todo.Models.Mappings;

namespace Todo.Models.Common
{
    public class CreatedAuditInfo : IMaps<ICreatedAudit>
    {
        public string By { get; set; }
        public DateTime On { get; set; }
        public string Process { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ICreatedAudit, CreatedAuditInfo>()
                .ForMember(m => m.By, o => o.MapFrom(m => m.CreatedBy))
                .ForMember(m => m.On, o => o.MapFrom(m => m.CreatedOn))
                .ForMember(m => m.Process, o => o.MapFrom(m => m.CreatedProcess));
        }
    }
}