using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MembershipPortal.Models
{
    public class Member
    {
        [Column("MemberId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Member name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for member's first name is 30 characters.")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Member middle name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for member's middle  name is 30 characters.")]
        public string middleName { get; set; }
        [Required(ErrorMessage = "Member last name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for member's last name is 30 characters.")]
        public string lasttName { get; set; }
        [Required(ErrorMessage = "Member gender is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for gender is 10 characters.")]
        public string gender { get; set; }
        public string city { get; set; }
        [Column("memberAge")]
        public DateTime dateofBirth { get; set; }
        public string academicRank { get; set; }
        public string academicTitle { get; set; }
        public string fieldofStudy { get; set; }
        public string speciality { get; set; }
        public string interestArea { get; set;}
        public string hostInstitution { get; set; }
        public Member refrencePerson { get; set; }
        
        [PhotoValidate (ErrorMessage= "Photo is Either >2MB or Its type is other than allowed Defaults")]
        public string memberPhotoURL { get; set; }
        public string memberCertificateURL { get; set; }
        public string memberCVURL { get; set; }
        public IEnumerable<Publication> memberPublications { get; set; }
    }
}
