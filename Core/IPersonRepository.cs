using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Core
{
    public interface IPersonRepository
    {
        void Add(Person person);
        Task<Person> GetPerson(int id, bool includeRelated);
        void RemovePhoneNumbers(ICollection<PhoneNumber> phoneNumbers);
        void Remove(Person person);
        Task<QueryResult<Person>> GetPersons(PersonQuery queryObj);
    }
}