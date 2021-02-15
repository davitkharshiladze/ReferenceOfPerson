using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Controllers.Resources
{
    public class RelatedPersonResource
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RelationshipType? RelationshipType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public ICollection<PhoneNumberResource> PhoneNumbers { get; set; }

        public RelatedPersonResource()
        {
            PhoneNumbers = new Collection<PhoneNumberResource>();
        }
    }
}
