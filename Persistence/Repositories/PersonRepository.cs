using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using ReferenceOfPerson.Core;
using ReferenceOfPerson.Core.Models;


namespace ReferenceOfPerson.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ReferenceOfPersonDbContext _context;

        public PersonRepository(ReferenceOfPersonDbContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
        }

        public async Task<Person> GetPerson(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Persons.FindAsync(id);

            return await _context.Persons.Include(p => p.Relationships.Select(r => r.RelatedPerson))
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public void RemovePhoneNumbers(ICollection<PhoneNumber> phoneNumbers)
        {
            _context.PhoneNumbers.RemoveRange(phoneNumbers);

        }

        public void Remove(Person person)
        {
            _context.Persons.Remove(person);
        }

        public async Task<QueryResult<Person>> GetPersons(PersonQuery queryObj)
        {
            var result = new QueryResult<Person>();

            var query = _context.Persons.AsQueryable();

            //Searching
            if (!String.IsNullOrWhiteSpace(queryObj.SearchTerm))
                query = query.Where(p => p.FirstName.Contains(queryObj.SearchTerm) ||
                                         p.LastName.Contains(queryObj.SearchTerm) ||
                                         p.PersonalNumber.Contains(queryObj.SearchTerm));


            //Filtering
            if (queryObj.Gender.HasValue)
                query = query.Where(p => p.Gender == queryObj.Gender);
            query = query.Where(p =>
                p.Birthdate.Value.Year >= queryObj.MinYearOfBirth && p.Birthdate.Value.Year <= queryObj.MaxYearOfBirth);


            //Ordering
            var columnsMap = new Dictionary<string, Expression<Func<Person, object>>>()
            {
                ["firstName"] = p => p.FirstName,
                ["lastName"] = p => p.LastName,
                ["birthdate"] = p => p.Birthdate,
                ["id"] = p => p.Id
            };

            if (!string.IsNullOrWhiteSpace(queryObj.SortBy) && columnsMap.ContainsKey(queryObj.SortBy))
            {
                query = queryObj.IsSortAscending ? query.OrderBy(columnsMap[queryObj.SortBy]) : query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            result.TotalItems = await query.CountAsync();

            //Paging 
            query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
