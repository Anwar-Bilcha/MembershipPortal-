using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
   public interface IPublicationRepository
    {
        public Publication GetPublication(Guid publicationId);
        public IEnumerable<Publication> GetPublications();
        public Publication CreatePublication(Guid memberId, Publication publication);
        public Publication DeletePublication(Guid publicationId);
        public Publication EditPublication(Guid publicationId);
        public IQueryable<Publication> GetPublications(Guid memberId);



    }
}
