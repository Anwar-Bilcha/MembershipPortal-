using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class PublicationUpdateDto
    {
        public string pubSubject { get; set; }
        public string pubDOI { get; set; }
        public string pubTitle { get; set; }
        public DateTime publicationDate { get; set; }
        public string coAuthors { get; set; }
        public string pubwebLink { get; set; }
        public Guid contributingMemberId { get; set; }
    }
}
