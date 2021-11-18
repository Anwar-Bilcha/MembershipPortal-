using MembershipPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public interface IMemberRepository
    {
        public Member AddMember(Member member);
         public Task<Member> GetMember(Guid Id);
         public Task<Member> UpdateProfile(Guid memberId, Member member);
        public Member AttachFile(Member member, string fileURL, string theDocType);
         public Task<IEnumerable<Member>> GetAllMembers();
        Member DeleteMember(Member member);
    }
}
