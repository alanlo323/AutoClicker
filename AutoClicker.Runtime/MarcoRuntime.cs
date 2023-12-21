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

        public void RunMarco(List<MarcoEvent> marcoEvents)
        {
            foreach (MarcoEvent marcoEvent in marcoEvents)
            {
                try
                {
                    marcoEvent.Excute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
