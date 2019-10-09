using MyIhsan.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Parameters
{
    public interface IBaseParam
    {
         int PageNumber { get; set; }
         int PageSize { get; set; }
         IEnumerable<SortModel> OrderByValue { get; set; }
    }
}
