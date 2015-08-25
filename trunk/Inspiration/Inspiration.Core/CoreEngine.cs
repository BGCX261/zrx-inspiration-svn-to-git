using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspiration.Core.DependencyManagement;
using Autofac;

namespace Inspiration.Core
{
    public class CoreEngine
    {
        private ContainerManager _containerManager;

        public CoreEngine()
		{
            ContainerConfigurer configurer = new ContainerConfigurer();
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager);
		}
        #region Methods
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public Array ResolveAll(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion
        #region Properties

        public IContainer Container
        {
            get { return _containerManager.Container; }
        }

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
