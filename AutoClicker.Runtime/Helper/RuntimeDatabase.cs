using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.Runtime.Helper
{
    public class RuntimeDatabase
    {
        public static Dictionary<object, object> Default { get; } = new();
        public static Dictionary<object, object> CreateRuntimeDatabase() { return []; }
    }
}
