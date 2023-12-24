using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Runtime.Core;
using static AutoClicker.Runtime.Core.MarcoEvent;

namespace AutoClicker.Runtime
{
    public class MarcoRuntime
    {
        private readonly Dictionary<object, object> runtimeDatabase;

        public MarcoRuntime()
        {
            runtimeDatabase = Helper.RuntimeDatabase.CreateRuntimeDatabase();
        }

        public async Task RunMarco(List<MarcoEvent> marcoEvents, MarcoEventStatusChangedEventHandler handler)
        {
            foreach (MarcoEvent marcoEvent in marcoEvents)
            {
                try
                {
                    await marcoEvent.Excute(runtimeDatabase, handler);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
