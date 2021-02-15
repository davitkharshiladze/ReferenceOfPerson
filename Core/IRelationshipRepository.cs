using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Core
{
    public interface IRelationshipRepository
    {
        void Add(Relationship relationship);
        void RemoveRange(IEnumerable<Relationship> relationships);
        void Remove(Relationship relationship);
        Task<IEnumerable<Relationship>> GetPersonRelationships(int personId);
        Task<Relationship> GetRelationship(Relationship relationship);
        //int personId, int relatedPersonId, RelationshipType relationshipType
    }
}