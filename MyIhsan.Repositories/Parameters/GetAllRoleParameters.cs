using MyIhsan.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Parameters
{
    public class GetAllRoleParameters:RoleSearchParameters,IBaseParam
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SortModel> OrderByValue { get; set; }
    }
}
