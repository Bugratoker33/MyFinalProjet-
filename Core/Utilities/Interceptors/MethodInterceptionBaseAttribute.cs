using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, Castle.DynamicProxy.IInterceptor
    {                         //burasıbase
        public int Priority { get; set; } //öncelik öbce loglama sonra validate gibi 

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }

}

