using Reservation.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.Parameters
{
    public class BaseParam
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SortModel> OrderByValue { get; set; }
    }
}
