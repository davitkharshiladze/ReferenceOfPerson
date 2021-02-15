using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ReferenceOfPerson.Core.Models;
using ReferenceOfPerson.Core.Utilities;

namespace ReferenceOfPerson.Controllers.Resources
{
    public class PersonResource
    {
        public int  Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [EngOrGeoLetters(ErrorMessage = "FirstName must be only Geo or Eng letters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [EngOrGeoLetters(ErrorMessage = "LastName must be only Geo or Eng letters")]
        public string LastName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "PersonalNumber must be a 11 digit")]
        public string PersonalNumber { get; set; }

        [Required]
        [Min18years]
        public DateTime? Birthdate { get; set; }

        public ICollection<PhoneNumberResource> PhoneNumbers { get; set; }
        public ICollection<RelatedPersonResource> RelatedPersons { get; set; }

        public PersonResource()
        {
            PhoneNumbers = new Collection<PhoneNumberResource>();
            RelatedPersons = new Collection<RelatedPersonResource>();
        }
    }
}