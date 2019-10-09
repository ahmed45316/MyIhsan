using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public static class Helper<T> where T:class
    {
        public static T Convert(object data)
        {
           return JsonConvert.DeserializeObject<T>(data.ToString());
        }
        public static T Convert(string data)
        {
            return JsonConvert.DeserializeObject<T>(data.ToString());
        }
    }
}
