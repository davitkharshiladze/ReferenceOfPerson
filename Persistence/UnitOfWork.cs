using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReferenceOfPerson.Controllers;
using ReferenceOfPerson.Core;
using ReferenceOfPerson.Persistence.Repositories;

namespace ReferenceOfPerson.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReferenceOfPersonDbContext _context;

        public IPersonRepository Persons { get; private set; }
        public IRelationshipRepository Relationships { get; private set; }

        public UnitOfWork(ReferenceOfPersonDbContext context)
        {
            _context = context;
            Persons = new PersonRepository(context);
            Relationships = new RelationshipRepository(context);
        }

        public async Task CompleteAsync()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
