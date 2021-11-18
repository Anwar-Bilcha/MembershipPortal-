using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string memberName { get; set; }
        public string profession { get; set; }
        public string qualification { get; set; }

        public static implicit operator HttpContent(MemberDto v)
        {
            throw new NotImplementedException();
        }
    }
}
