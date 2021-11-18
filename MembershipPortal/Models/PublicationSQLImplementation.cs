using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class PublicationSQLImplementation : IPublicationRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public PublicationSQLImplementation(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        
        Publication IPublicationRepository.CreatePublication(Guid memberId, Publication publication)
        {
           publication.contributingMemberId = memberId; 
          _repositoryContext.Publications.Add(publication);
            _repositoryContext.SaveChanges();
            return publication; 
        }

        Publication IPublicationRepository.EditPublication(Guid publicationId)
        {
            var PubSearched = _repositoryContext.Publications.Find(publicationId);
            var pubonTheFly = _repositoryContext.Publications.Attach(PubSearched);

            pubonTheFly.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _repositoryContext.SaveChanges();
            return PubSearched;
        }

              
        Publication IPublicationRepository.DeletePublication(Guid publicationId)
        {
            Publication pub2Delete = _repositoryContext.Publications.Find(publicationId); 
            _repositoryContext.Publications.Remove(pub2Delete);
                _repositoryContext.SaveChanges();
            return pub2Delete;
        }

       
    IEnumerable<Publication> IPublicationRepository.GetPublications()
        {
            return _repositoryContext.Publications.ToList();
        }

        Publication IPublicationRepository.GetPublication(Guid publicationId)
        {
            var PubSearched = _repositoryContext.Publications.Find(publicationId);
            return PubSearched;
                
        }

        IQueryable<Publication> IPublicationRepository.GetPublications(Guid memberId)
        {
            Guid cmI = memberId;
            var x = _repositoryContext.Publications.Where(c => c.contributingMemberId == cmI); 
            //var IntendedResult = intermediaryList.FindAll(c => c.contributingMemberId == memberId);
            //return IntendedResult;
            return x.OrderBy(c=>c.pubTitle);
        }
    }
}
