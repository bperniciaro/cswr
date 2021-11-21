using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cswr.Web.Models.ViewModels
{
    public class ContactViewModel
    {

        public enum ContactInquiryType
        {
            Common,
            Question,
            Error,
            Advertising,
            Other
        }

        //public class ContactFormTypeOption
        //{
        //    public int Id { get; set; }
        //    public string Type { get; set; }

        //    public ContactFormTypeOption(int id, string type)
        //    {
        //        this.Id = id;
        //        this.Type = type;
        //    }
        //}

        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public List<ContactInquiryType> ContactInquiryTypes { get; set; }
        public ContactInquiryType ContactType { get; set; }

    }
}
