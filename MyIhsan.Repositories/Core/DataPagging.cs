using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public class DataPagging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public ResponseResult Result { get; set; }
    }
}
