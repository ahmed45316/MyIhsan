using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public class SortModel
    {
        public string ColId { get; set; }
        public string Sort { get; set; }
        public string PairAsSqlExpression
        {
            get
            {
                return $"{ColId} {Sort}";
            }
        }
    }
}
