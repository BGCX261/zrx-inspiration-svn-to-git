using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;

namespace Inspiration.Core.Extension
{
    public class MefDependencySolver : IDependencyResolver
    {
        public MefDependencySolver(CompositionContainer compositionContainer)
        {
            _compositionContainer = compositionContainer;
        }

        private readonly CompositionContainer _compositionContainer;

        public object GetService(Type serviceType)
        {
            var name = AttributedModelServices.GetContractName(serviceType);
            return _compositionContainer.GetExportedValueOrDefault<object>(name);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _compositionContainer
                       .GetExportedValues<object>(serviceType.FullName);
        }
    }
}
