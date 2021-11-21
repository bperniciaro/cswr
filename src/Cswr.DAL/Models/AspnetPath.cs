using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class AspnetPath
    {
        public AspnetPath()
        {
            AspnetPersonalizationPerUsers = new HashSet<AspnetPersonalizationPerUser>();
        }

        public Guid ApplicationId { get; set; }
        public Guid PathId { get; set; }
        public string Path { get; set; }
        public string LoweredPath { get; set; }

        public virtual AspnetApplication Application { get; set; }
        public virtual AspnetPersonalizationAllUser AspnetPersonalizationAllUser { get; set; }
        public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers { get; set; }
    }
}
