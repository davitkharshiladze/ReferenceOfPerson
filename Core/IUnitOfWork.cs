using System.Threading.Tasks;

namespace ReferenceOfPerson.Core
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }
        IRelationshipRepository Relationships { get; }
        Task CompleteAsync();
    }
}