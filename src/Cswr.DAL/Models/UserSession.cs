using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class UserSession
    {
        public Guid UserId { get; set; }
        public int SessionCount { get; set; }

        public virtual AspnetMembership User { get; set; }
    }
}
