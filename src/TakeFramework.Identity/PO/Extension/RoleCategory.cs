using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Identity.PO.Extension
{
    public class RoleCategory : FullAuditEntity<long, long>
    {
        public string Name { get; set; }
    }
}

