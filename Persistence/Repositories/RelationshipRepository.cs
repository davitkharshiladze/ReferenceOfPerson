using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ReferenceOfPerson.Core;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Persistence.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly ReferenceOfPersonDbContext _context;
        public RelationshipRepository(ReferenceOfPersonDbContext context)
        {
            _context = context;
        }

        public void Add(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
        }

        public void RemoveRange(IEnumerable<Relationship> relationships)
        {
            _context.Relationships.RemoveRange(relationships);
        }

        public async Task<IEnumerable<Relationship>> GetPersonRelationships(int personId)
        {
            return await _context.Relationships.Where(r => r.PersonId == personId || r.RelatedPersonId == personId)
                .ToListAsync();
        }

        public async Task<Relationship> GetRelationship(Relationship relationship)
        {
            return await _context.Relationships.SingleOrDefaultAsync(r =>
                r.PersonId == relationship.PersonId && r.RelatedPersonId == relationship.RelatedPersonId && r.Type == relationship.Type);
        }

        public void Remove(Relationship relationship)
        {
            _context.Relationships.Remove(relationship);
        }
    }
}
