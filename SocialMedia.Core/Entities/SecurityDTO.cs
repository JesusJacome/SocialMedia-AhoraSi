using SocialMedia.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public class SecurityDTO
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public RoleType Role { get; set; }
    }
}
