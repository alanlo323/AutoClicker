using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Runtime.Core;
using AutoClicker.Runtime.Script;

namespace AutoClicker.UI.Script
{
    internal class BaseScipt
    {
        public virtual string Name { get; set; } = nameof(BaseScipt);
        public virtual List<MarcoEvent> MarcoEvents { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
