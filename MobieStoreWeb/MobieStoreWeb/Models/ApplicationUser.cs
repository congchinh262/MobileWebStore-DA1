using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobieStoreWeb.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual ICollection<ProductComment> ProductComments{ get; set; }
    }
}
