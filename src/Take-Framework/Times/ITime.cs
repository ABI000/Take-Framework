using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Times
{
    public interface ITime
    {
        public DateTime Now { get; }

        public DateTime UtcNow { get; }

        public DateTime GetLocalTimeDateTime(DateTime dateTime);
    }
}
