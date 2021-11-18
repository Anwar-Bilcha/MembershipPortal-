using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class AppUsers : IdentityUser
    {
string City { get; set; }
    }
}
