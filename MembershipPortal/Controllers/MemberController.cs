using AutoMapper;
using MembershipPortal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MembershipPortal.Controllers
{
    [EnableCors("CorPol")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly RepositoryContext _repositoryContext;

        public MemberController(IMemberRepository memberRepository, RepositoryContext repositoryContext)
        {
            _memberRepository = memberRepository;
            _repositoryContext = repositoryContext;

        }


        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _memberRepository.GetAllMembers();
            if (members == null)
                return NotFound();
            var memberDt = Members2MemberDto(members);
            return Ok(memberDt);
        }




        [HttpPut("id")]
        [Route ("{id}")]
        public async Task<IActionResult> UpdateMember([FromBody] Member memb, [FromRoute]Guid id)
        {
            if (memb == null)
            {
                return NotFound();
            }
            var checkifExistsIdOwner =  _repositoryContext.Members.FirstOrDefault(c => c.Id == id);
            if(checkifExistsIdOwner == null)
            {
                var x = new HttpResponseMessage();
                x.Headers.Add("NoExists", " The Member with if {id} is not in the Database");
                return Ok(x);
            }
            var responseforClient = await _memberRepository.UpdateProfile(id, memb);
            return Ok(responseforClient);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetMember(Guid id)
        //{
        //     var member = await _memberRepository.GetMember(id);
        //    if (member == null)
        //        return NotFound();
        //    var memberasList = new List<Member>();
        //    memberasList.Add(member);
        //    var memberDto = Member2MemberDto(memberasList);
        //    return Ok(memberDto);
        //}



        [HttpPost]     
        public IActionResult AddMember([FromBody]Member member)
        {
            if (member == null)
                return BadRequest("Inserting an empty Object is not allowed!");
            try
            {
             var x = _memberRepository.AddMember(member);
             _repositoryContext.SaveChanges();
                var wrapx = new List<Member>();
                wrapx.Add(member);
                var result = Member2MemberDto(wrapx);
             return Ok(result); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong in the {nameof(AddMember)}  action { ex} .");
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpDelete("{id}")]
       
        public IActionResult DeleteMember([FromHeader]Guid id)
        {
            var response = new HttpResponseMessage();
            var member = _repositoryContext.Members.Find(id);
            if (member == null)
            {
               
                response.Headers.Add("DeleteMessage", "Such a member requested to be Deleted is not in the Database!!!");
                return Ok(response);
            }
            _memberRepository.DeleteMember(member);
            response.Headers.Add("DeleteMessage", "Successfully Deleted!!!");
            return Ok(response);
           
        }


        private MemberDto makeCreateable(MemberCreateDto member)
        {
            var id = new Guid();
            MemberDto result = new MemberDto();
            result.Id = id;
            result.memberName = member.memberName;
            result.profession = member.profession;
            result.qualification = member.qualification;
            return result;
        }

        IEnumerable<MemberDto> Members2MemberDto(IEnumerable<Member> members)
        {
             
            var memberDto = members.Select(c => new MemberDto
            {
                Id = c.Id,
                memberName = string.Join(' ', c.firstName, c.middleName, c.lasttName),
                qualification = string.Join(' ', c.academicRank, c.fieldofStudy, c.academicTitle),
                profession = string.Join(' ', c.speciality, c.interestArea)
            });
            return memberDto.ToList();

        }
        MemberDto Member2MemberDto(IEnumerable<Member> members)
        {
            
            var memberDto = members.Select(c => new MemberDto
            {
                Id = c.Id,
                memberName = string.Join(' ', c.firstName, c.middleName, c.lasttName),
                qualification = string.Join(' ', c.academicRank, c.fieldofStudy, c.academicTitle),
                profession = string.Join(' ', c.speciality, c.interestArea)
            });
            return memberDto.Last();

        }
    }
}
