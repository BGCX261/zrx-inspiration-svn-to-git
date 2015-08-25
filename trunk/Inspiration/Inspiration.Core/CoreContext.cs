using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Inspiration.Core.Singleton;
using System.Diagnostics;

namespace Inspiration.Core
{
    public class CoreContext
    {
        /// <summary>Gets the singleton Nop engine used to access Nop services.</summary>
        public static CoreEngine Current
        {
            get
            {
                if (Singleton<CoreEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<CoreEngine>.Instance;
            }
        }
        /// <summary>Initializes a static instance of the Nop factory.</summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static CoreEngine Initialize(bool forceRecreate)
        {
            if (Singleton<CoreEngine>.Instance == null || forceRecreate)
            {
                Debug.WriteLine("Constructing engine " + DateTime.Now);
                Singleton<CoreEngine>.Instance = new CoreEngine();
                Debug.WriteLine("Initializing engine " + DateTime.Now);
            }
            return Singleton<CoreEngine>.Instance;
        }
    }
}
