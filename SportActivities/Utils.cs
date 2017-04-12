using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportActivities
{
    public static class Utils
    {
        public static string beautify(string param)
        {
            string result = param.First().ToString().ToUpper() + param.Substring(1);

            result = result.Replace("_", " ");
            return result;
        }
    }
}
