using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Common
{
    public static class Helper
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
