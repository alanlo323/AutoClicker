using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Runtime.Core;

namespace AutoClicker.UI.Script
{
    internal class BaseScipt
    {
        public virtual string Name { get; set; } = nameof(BaseScipt);
        public virtual List<MarcoEvent> MarcoEvents { get; }

        public List<MarcoEvent> GetAllMarcoEvents()
        {
            List<MarcoEvent> marcoEvents = [];
            foreach (var marcoEvent in MarcoEvents)
            {
                marcoEvents.AddRange(marcoEvent.GetAllSubEvents());
            }
            return marcoEvents;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
