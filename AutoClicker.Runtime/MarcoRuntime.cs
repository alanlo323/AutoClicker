using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Runtime.Core;

namespace AutoClicker.Runtime
{
    public class MarcoRuntime
    {
        private readonly Dictionary<object, object> runtimeDatabase;

        public MarcoRuntime()
        {
            runtimeDatabase = Helper.RuntimeDatabase.CreateRuntimeDatabase();
        }

        public void RunMarco(List<MarcoEvent> marcoEvents)
        {
            foreach (MarcoEvent marcoEvent in marcoEvents)
            {
                try
                {
                    marcoEvent.Excute(runtimeDatabase);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
