using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportActivities.DataModels
{
    public class Query
    {
        private string tableName;
        private string condition;
        private string relation;

        public Query()
        {
            tableName = null;
            condition = null;
            relation = null;
        }

        public string TableName
        {
            get
            {
                return this.tableName;
            }
            set
            {
                this.tableName = value;
            }
        }

        public string Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value;
            }
        }
    }
}
