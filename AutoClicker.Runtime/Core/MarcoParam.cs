using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.Runtime.Core
{
    public class MarcoParam
    {
        private string _dummy;

        public MarcoEvent MarcoEvent { get; set; }
        public PropertyInfo Property { get; set; }
        public string PropertyName { get => Property.Name; set => _dummy = value; }
        public string PropertyValue { get => $"{Property.GetValue(MarcoEvent)}"; set => _dummy = value; }
    }
}
