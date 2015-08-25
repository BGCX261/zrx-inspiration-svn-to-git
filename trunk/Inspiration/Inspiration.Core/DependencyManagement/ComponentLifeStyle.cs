using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspiration.Core.DependencyManagement
{
    public enum ComponentLifeStyle
    {
        Singleton = 0,
        Transient = 1,
        LifetimeScope = 2
    }
}
