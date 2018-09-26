using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.TableCacheDependency
{
    public class Menus : MsSqlCacheDependency
    {
        public Menus() : base("MenusTableDependency") { }
    }
}