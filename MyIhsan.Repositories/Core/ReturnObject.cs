using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public  class ReturnObject
    {
        public List<SortModel> GetSortModels(string sortColumn,string sortColumnDir)
        {
            return  new List<SortModel>() {
            new SortModel()
            {
                ColId = sortColumn,
                Sort = sortColumnDir
            }
            };
        }
    }
}
