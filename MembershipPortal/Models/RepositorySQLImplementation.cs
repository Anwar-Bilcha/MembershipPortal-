using MembershipPortal.Models;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class RepositorySQLImplementation : IMemberRepository 
    {
        private readonly RepositoryContext _contextRepository; 
        public RepositorySQLImplementation(RepositoryContext repositoryContext)
        {
            _contextRepository = repositoryContext;
        }
        Member IMemberRepository.AddMember(Member member)
        {
            _contextRepository.Members.Add(member);
            return member;

        }

        Member IMemberRepository.AttachFile(Member member, string fileURL,string theDocType)
        {
            string col = (theDocType == member.memberCertificateURL) ? member.memberCertificateURL : member.memberCVURL;
            if (col==member.memberCertificateURL)
            {
                member.memberCertificateURL =fileURL;

                _contextRepository.Members.UpdateRange();
                
            }
            return member;
        }

        async Task<Member> IMemberRepository.GetMember(Guid Id) =>
        
             await _contextRepository.Members.FindAsync(Id); 
        

       async  Task<Member> IMemberRepository.UpdateProfile(Guid memberId, Member member)
        {
            var mem = await _contextRepository.Members.FindAsync(memberId);
            var trackings = _contextRepository.Members.Attach(mem);
            trackings.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            mem.Id = memberId;
            mem.academicRank = member.academicRank;
            mem.academicTitle = member.academicTitle;
            mem.city = member.city;
            mem.dateofBirth = member.dateofBirth;
            mem.fieldofStudy = member.fieldofStudy;
            mem.gender = member.gender;
            mem.hostInstitution = member.hostInstitution;
            mem.firstName = member.firstName;
            mem.middleName = member.middleName;
            mem.lasttName = member.lasttName;
            mem.memberCertificateURL = member.memberCertificateURL;
            mem.memberCVURL = member.memberCVURL;
            mem.interestArea = member.interestArea;
            mem.memberPublications = member.memberPublications;
            mem.memberPhotoURL = member.memberPhotoURL;
            await _contextRepository.SaveChangesAsync();
            return mem;
        }
        async Task<IEnumerable<Member>> IMemberRepository.GetAllMembers() =>

            await _contextRepository.Members.OrderBy(c => c.firstName).ToListAsync();

         Member IMemberRepository.DeleteMember(Member member)
        {
            _contextRepository.Members.Remove(member);
            _contextRepository.SaveChanges();
            return member;
        }
    }
}
