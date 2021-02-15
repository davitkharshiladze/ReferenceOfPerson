using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Models
{
    public class Relationship
    {
        [Required]
        [EnumDataType(typeof(RelationshipType))]
        public RelationshipType? Type { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }

    }
}