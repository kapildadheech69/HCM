using LoginAndRegistration.Dto;
using LoginAndRegistration.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;

namespace LoginAndRegistration.Controllers
{
    [ApiController]
    [Route("HealthCare")]
    public class LoginAndRegistrationController:ControllerBase
    {
        private ToDoContext context;
        public LoginAndRegistrationController(ToDoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetPhysicians")]
        public IEnumerable<Physician> GetPhysicianList()
        {
            var physicians = context.Physicians.ToList();
            return physicians;
        }

        [HttpPost]
        [Route("AddMember")]
        public ActionResult CreateMember(MemberDto memberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Member member = new Member();
            member.FirstName = memberDto.FirstName;
            member.LastName = memberDto.LastName;   
            member.UserName = memberDto.UserName; 
            member.Password = memberDto.Password;
            member.ConfirmPassword = memberDto.ConfirmPassword; 
            member.Address = memberDto.Address;
            member.State = memberDto.State;
            member.City = memberDto.City;
            member.Email = memberDto.Email;

            member.PhysicianId = 5;
            member.DateOfBirth = memberDto.DateOfBirth; 
            member.CreationDate = DateTime.Now; 
            member.LastModificationDate=DateTime.Now;
            
            context.Members.Add(member);
            context.SaveChanges();

            return Created(new Uri(Request.Headers + "/" + member.MemberId), memberDto);
        }

        [HttpPost]
        [Route("SubmitClaim")]
        public ActionResult SubmitClaim(ClaimDto claimDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            Claim claim = new Claim();
            claim.ClaimType = claimDto.ClaimType;   
            claim.ClaimAmount = claimDto.ClaimAmount;  
            claim.Remarks = claimDto.Remarks;
            claim.MemberId = claimDto.MemberId;

            claim.ClaimDate = DateTime.Now;

            context.Claims.Add(claim);
            context.SaveChanges();

            return Created(new Uri(Request.Headers + "/" + claim.ClaimId), claimDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ByMemberId/{id}")]
        public ActionResult<Member> GetMemberByMemberId(int id)
        {
            var member = context.Members.SingleOrDefault(m => m.MemberId == id);
            if(member==null)
            {
                return BadRequest("Invalid Id");
            }
            var  memberDto = new SearchMemberDto();
            memberDto.FirstName = member.FirstName;
            memberDto.LastName = member.LastName;
            memberDto.UserName = member.UserName;
            memberDto.Address = member.Address;
            memberDto.State = member.State;
            memberDto.City = member.City;
            memberDto.Email = member.Email;
            memberDto.DateOfBirth = member.DateOfBirth;

            return Ok(memberDto);
        }

        [HttpGet]
        [Route("ByFirstNameAndLastName")]
        public ActionResult GetMemberByFirstNameAndLastName(string firstName = null, string lastName = null)
        {
            var member=context.Members.SingleOrDefault(m => m.FirstName == firstName && m.LastName == lastName);
            if (member == null)
                return BadRequest();

            var memberDto = new SearchMemberDto();
            memberDto.FirstName = member.FirstName;
            memberDto.LastName = member.LastName;
            memberDto.UserName = member.UserName;
            memberDto.Address = member.Address;
            memberDto.State = member.State;
            memberDto.City = member.City;
            memberDto.Email = member.Email;
            memberDto.DateOfBirth = member.DateOfBirth;

            return Ok(memberDto);
        }

        [HttpGet]
        [Route("ByClaimId/{id}")]
        public ActionResult GetMemberByClaimId(int id)
        {
            var member = context.Members.SingleOrDefault(m => m.Claims.Any(c => c.ClaimId == id));
            if (member == null)
                return BadRequest();
            var memberDto = new SearchMemberDto();

            memberDto.FirstName = member.FirstName;
            memberDto.LastName = member.LastName;
            memberDto.UserName = member.UserName;
            memberDto.Address = member.Address;
            memberDto.State = member.State;
            memberDto.City = member.City;
            memberDto.Email = member.Email;
            memberDto.DateOfBirth = member.DateOfBirth;

            return Ok(memberDto);
        }

        [HttpGet]
        [Route("ByPhysicianName")]

        public ActionResult<SearchMemberDto> GetMemberByPhysicianname(string name=null)
        {
            var physician = context.Physicians.Include(p => p.Members).SingleOrDefault(p => p.PhysicianName == name);
            if (physician == null)
                return BadRequest();
            List<SearchMemberDto> membersDto=new List<SearchMemberDto>();
            SearchMemberDto memberDto=new SearchMemberDto();
            foreach (var member in physician.Members)
            {
                memberDto.FirstName = member.FirstName;
                memberDto.LastName = member.LastName;
                memberDto.UserName = member.UserName;
                memberDto.Address = member.Address;
                memberDto.State = member.State;
                memberDto.City = member.City;
                memberDto.Email = member.Email;
                memberDto.DateOfBirth = member.DateOfBirth;
                membersDto.Add(memberDto);
            }
            return Ok(membersDto);
        }
    }
}
