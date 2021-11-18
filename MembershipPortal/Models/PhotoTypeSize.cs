using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipPortal.Models
{
    public class PhotoValidate : ValidationAttribute 
    {
        public PhotoValidate(string PhtoAdd, AllowedImages PhotoFT, int PhotoSz)
        {
            PhotoAddress = PhtoAdd;
            PhotoFileType = PhotoFT;
            Photosize = PhotoSz;
        }
        public PhotoValidate()
        {
                
        }
        public int Photosize { get; set; }
        public string PhotoAddress { get; set; }
        public  AllowedImages PhotoFileType;
        public override bool IsValid(object value)
        {
            return (Photosize <= 2) && (PhotoFileType.ToString().ToUpper() == AllowedImages.GIF.ToString());
        }
    }
}
