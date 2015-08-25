using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Inspiration.Core.TypeFinder;

namespace Inspiration.Core.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
