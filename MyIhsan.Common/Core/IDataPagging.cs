﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Common.Core
{
    public interface IDataPagging
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPage { get; set; }
        IResponseResult Result { get; set; }
    }
}
