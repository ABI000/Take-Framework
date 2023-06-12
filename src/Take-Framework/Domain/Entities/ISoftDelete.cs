using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Domain.Entities
{
    public interface ISoftDelete
    {
        bool Delete { get; set; }
    }
}
