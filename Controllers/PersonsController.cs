using Microsoft.AspNetCore.Mvc;
using ReferenceOfPerson.Core.Models;
using AutoMapper;
using ReferenceOfPerson.Controllers.Resources;
using System.Threading.Tasks;
using ReferenceOfPerson.Core;


namespace ReferenceOfPerson.Controllers
{
    [Route("/api/persons")]
    public class PersonsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonResource personResource)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = _mapper.Map<PersonResource, Person>(personResource);
            _unitOfWork.Persons.Add(person);

             await _unitOfWork.CompleteAsync();

            personResource = _mapper.Map<Person,PersonResource>(person);
            return Ok(personResource);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonResource personResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = await _unitOfWork.Persons.GetPerson(id, includeRelated:false);
            if (person == null)
                return NotFound("Person Not Found");

            _unitOfWork.Persons.RemovePhoneNumbers(person.PhoneNumbers);

            _mapper.Map(personResource,person);

            await _unitOfWork.CompleteAsync();

            personResource.Id = person.Id;

            return Ok(personResource);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _unitOfWork.Persons.GetPerson(id, includeRelated: false);
            if (person == null)
                return NotFound("Person not found");

            var relationships = await _unitOfWork.Relationships.GetPersonRelationships(person.Id);

            _unitOfWork.Relationships.RemoveRange(relationships);

            _unitOfWork.Persons.Remove(person);

            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _unitOfWork.Persons.GetPerson(id, includeRelated: true);
            
            if (person == null)
                return NotFound("Person not found");

            var personResource = _mapper.Map<Person, PersonResource>(person);

            return Ok(personResource);
        }


        public async Task<QueryResultResource<PersonResource>> GetPersons([FromQuery] PersonQuery queryObj)
        {
            var queryResult = await _unitOfWork.Persons.GetPersons(queryObj);

            return _mapper.Map<QueryResult<Person>, QueryResultResource<PersonResource>>(queryResult);
        }
    }
}