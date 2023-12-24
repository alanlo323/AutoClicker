using System;
using System.CodeDom;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.Runtime.Core
{
    public class MarcoParam
    {
        public object RefObject { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public string PropertyName { get => PropertyInfo.Name; }
        public object PropertyValue
        {
            get => $"{PropertyInfo.GetValue(RefObject)}";
            set 
            {
                PropertyInfo.SetValue(RefObject, value);
            }
        }
    }
}
