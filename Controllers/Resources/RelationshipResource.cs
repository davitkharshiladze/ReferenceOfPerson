using System.ComponentModel.DataAnnotations;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Controllers.Resources
{
    public class RelationshipResource
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }

        [Required]
        [EnumDataType(typeof(RelationshipType), ErrorMessage = "relation type is invalid")]
        public RelationshipType? Type { get; set; }
    }
}