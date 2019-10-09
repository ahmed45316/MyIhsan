using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Repositories.Core;

namespace MyIhsan.Repositories.Parameters
{
    public class GetAllUserParameters : UserSearchParameters,IBaseParam
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SortModel> OrderByValue { get; set; }
    }
}
