using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportStatusCode
    {
        public string SportCode { get; set; }
        public string StatusCode { get; set; }

        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual PlayerStatusCode StatusCodeNavigation { get; set; }
    }
}
