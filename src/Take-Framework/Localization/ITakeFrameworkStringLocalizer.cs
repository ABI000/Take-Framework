using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Localization
{
    public interface ITakeFrameworkStringLocalizer : IStringLocalizer
    {
        public string Tag { get; }
    }
}
