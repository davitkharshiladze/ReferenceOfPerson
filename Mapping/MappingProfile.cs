using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using ReferenceOfPerson.Controllers.Resources;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Mapping
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<PhoneNumber, PhoneNumberResource>();
            CreateMap<Person, RelatedPersonResource>();
            CreateMap<Person, PersonResource>()
                .ForMember
                (pr => pr.RelatedPersons, opt =>
                    opt.MapFrom(p => p.Relationships.Select(r => r.RelatedPerson)))
                .AfterMap((p, pr) =>
                {
                    foreach (var relatedPerson in pr.RelatedPersons)
                    {
                        relatedPerson.RelationshipType =
                            p.Relationships.SingleOrDefault(r => r.RelatedPersonId == relatedPerson.Id).Type;
                    }
                });
            


            // API Resource to Domain
            CreateMap<PersonResource, Person>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<PhoneNumberResource, PhoneNumber>();
            CreateMap<RelationshipResource, Relationship>();


        }
    }
}
