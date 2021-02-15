using Microsoft.AspNetCore.Mvc;
using ReferenceOfPerson.Core.Models;
using ReferenceOfPerson.Controllers.Resources;
using AutoMapper;
using System.Threading.Tasks;
using ReferenceOfPerson.Core;

namespace ReferenceOfPerson.Controllers
{
    [Route("/api/relationships")]
    public class RelationshipsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RelationshipsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRelationship([FromBody] RelationshipResource relationshipResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = await _unitOfWork.Persons.GetPerson(relationshipResource.PersonId, includeRelated:false);
            if (person == null)
                return NotFound();

            var relatedPerson = await _unitOfWork.Persons.GetPerson(relationshipResource.PersonId, includeRelated: false);
            if (relatedPerson == null)
                return NotFound();

            var relationship = _mapper.Map<RelationshipResource, Relationship>(relationshipResource);

            _unitOfWork.Relationships.Add(relationship);


            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRelationship([FromQuery] RelationshipResource relationshipResource)
        {
            var relationship = _mapper.Map<RelationshipResource, Relationship>(relationshipResource);

            var relationshipInDb = await _unitOfWork.Relationships.GetRelationship(relationship);
            if (relationshipInDb == null)
                return NotFound();

            _unitOfWork.Relationships.Remove(relationship);
            await _unitOfWork.CompleteAsync();
            return Ok();

        }
    }
}
