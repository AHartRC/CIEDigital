using System;
using System.Threading;
using System.Web.Mvc;
using CIEDigitalLib.Initializers;

namespace CIEDigitalLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class CIEDigitalDatabaseAttribute : ActionFilterAttribute
    {
        private static CIEDigitalDatabaseInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);

            //Enable per action logging of the database here?
        }
    }
}