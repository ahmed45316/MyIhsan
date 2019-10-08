using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Common.Core
{
   public interface IPrimaryKeyField<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}