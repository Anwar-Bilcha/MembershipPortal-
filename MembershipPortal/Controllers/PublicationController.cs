using MembershipPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MembershipPortal.Controllers
{
    [Route("api/Member/{memberId}/publication")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly RepositoryContext _repositoryContext;

        public PublicationController(IPublicationRepository publicationRepository, RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _publicationRepository = publicationRepository;
        }


        //[HttpGet]
        //public IActionResult GetPublications()
        //{
        //    var pubs = _publicationRepository.GetPublications();
        //    if (pubs == null)
        //        return NotFound();
        //    var Pubresponse = Publist2PubResponse(pubs);
        //    return Ok(Pubresponse);
        //}

        [HttpGet]
        //[Route("{id}")]
        public IActionResult GetPublications([FromRoute] Guid memberId)
        {
            var membersPubs = _publicationRepository.GetPublications(memberId);
            var membersPubresp = Publist2PubResponse(membersPubs);
            return Ok(membersPubresp);
        }


        [HttpGet("{id}", Name = "GetPublicationForMember")]
        [Route("{id}")]
        public IActionResult GetPublication([FromRoute] Guid id)
        {
            var publication = _publicationRepository.GetPublication(id);
            if (publication == null)
                return NotFound();
            var publicationasList = new List<Publication>();
            publicationasList.Add(publication);
            var publicationDto = Publist2PubResponse(publicationasList).Last();
            return Ok(publicationDto);
        }

        [HttpDelete]

        public IActionResult DeletePublication([FromHeader] Guid pubid)
        {
            _publicationRepository.DeletePublication(pubid);
            var response = new HttpResponseMessage();
            response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
            return Ok(response);
        }




        [HttpPut("{id}")]
        [Route ("{id}")]
        public IActionResult UpdatePublicationforMember([FromRoute] Guid memberId, [FromRoute] Guid id, [FromBody] Publication pud)
        {
           
            if (pud == null && pud.Id!=id)
            {
                 return BadRequest("EmployeeForUpdateDto object is null");
            }
            var MemberPublisher = _repositoryContext.Members.Find(memberId);
            if (MemberPublisher == null)
            {
                
            return NotFound();
            }
           
            pud.Id = id;
            var Publication2Update = _repositoryContext.Publications.Attach(pud);
            Publication2Update.State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            if (Publication2Update == null)
            {
            return NotFound();
            }
            //Publication2Update = _repositoryContext.Publications.Attach(pud);
            //Publication2Update.pubTitle = pud.pubTitle;
            //Publication2Update.pubSubject = pud.pubSubject;
            //Publication2Update.pubDOI = pud.pubDOI;
            //Publication2Update.publicationDate = pud.publicationDate;
            //Publication2Update.pubwebLink = pud.pubwebLink;
            //Publication2Update.contributingMemberId = pud.contributingMemberId;
            //Publication2Update.coAuthors = pud.coAuthors;
            //_repositoryContext.Publications.Update(pud);
            _repositoryContext.SaveChanges();
           
            return Ok(_repositoryContext.Publications.Find(id));
        }





        //public IActionResult UpdatePublicationforMember([FromRoute] Guid memberId, [FromRoute] Guid id, [FromBody] PublicationUpdateDto pud)
        //{
        //    if (pud == null)
        //    {
        //        return BadRequest("PublicationUpdateDto object is null");
        //    }
        //    var member = _repositoryContext.Members.Find(memberId);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }
        //    var publication = _repositoryContext.Publications.Find(id);
        //    // var PubSearched = _repositoryContext.Publications.Find(id);
        //    var pubonTheFly = _repositoryContext.Publications.Attach(publication);

        //    //pubonTheFly.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        //    if (publication == null)
        //    {
        //        return NotFound();
        //    }
        //    _repositoryContext.SaveChanges();
        //    var x = new HttpResponseMessage();
        //    x.Headers.Add("PublicationUpdate", "You have Successfully Updated a publication");
        //    return Ok(x);
        //}




        [HttpPost]
        public IActionResult CreatePublication(Guid memberId, [FromBody] Publication publication)
        {
            if (publication == null)
                return BadRequest("Inserting an empty Object is not allowed!");
            try
            {
                var member = _repositoryContext.Members.Find(memberId);
                if (member == null)
                {
                    return NotFound();
                }
                var createdPub = _publicationRepository.CreatePublication(memberId, publication);
                _repositoryContext.SaveChanges();
                var wrapcreatedPub = new List<Publication>();
                wrapcreatedPub.Add(publication);
                var result = Publist2PubResponse(wrapcreatedPub).Last();
                return CreatedAtRoute("GetPublicationForMember", new {
                    memberId, id = result.Id}, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong in the {nameof(CreatePublication)}  action { ex} .");
                return StatusCode(500, "Internal server error");
            }
        }




        private IEnumerable<PublicationResponseDto> Publist2PubResponse(IEnumerable<Publication> pubs)
        {
            var pubDto = pubs.Select(c => new PublicationResponseDto
            {
                Id = c.Id,
                pubDOI = c.pubDOI,
                publicationDate = c.publicationDate,
                pubwebLink = c.pubwebLink,
                contributingMemberId = c.contributingMemberId,
                coAuthors = c.coAuthors,
                pubTitleandArea = string.Join("(", c.pubTitle, c.pubSubject, ")")
            });
            return pubDto.ToList();
        }



    }
}
