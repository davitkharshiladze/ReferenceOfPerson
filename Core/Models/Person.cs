using ReferenceOfPerson.Core.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Models
{
    [Table("Persons")]
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]
        [EngOrGeoLetters]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [EngOrGeoLetters]
        public string LastName { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$")]
        public string PersonalNumber { get; set; }

        [Required]
        [Min18years]
        public DateTime? Birthdate { get; set; }

        public ICollection<Relationship> Relationships { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get;  set; }

        public Person()
        {
            PhoneNumbers = new Collection<PhoneNumber>();
            Relationships = new Collection<Relationship>();
        }

    }

       
}