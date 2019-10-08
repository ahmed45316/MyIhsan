using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Common.DbContextBase
{
    public abstract class BaseContext<T> : DbContext where T:DbContext
    {
        protected BaseContext(DbContextOptions<T> options) : base(options)
        {
            //Database.Migrate();
        }
    }
}
