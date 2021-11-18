using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class Publication
    {
        [Column("PublicationId")]
        public Guid Id { get; set; }
        public string pubSubject { get; set; }
        public string pubDOI { get; set; }
        public string pubTitle { get; set; }
        public DateTime publicationDate { get; set; }
        public string coAuthors { get; set; }
        public string pubwebLink { get; set; }

        [ForeignKey (nameof(Member))]
        public  Guid contributingMemberId { get; set; }






}
}
