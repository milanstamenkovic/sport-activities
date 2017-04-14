using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportActivities.DataModels
{
    public class Query
    {
        public static Query getInstance()
        {
            return new Query();
        }

        public string TableName { get; set; }

        public string Condition { get; set; }
    }
}
