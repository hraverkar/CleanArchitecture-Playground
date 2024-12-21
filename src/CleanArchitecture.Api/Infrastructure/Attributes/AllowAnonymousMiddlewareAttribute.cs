namespace CleanArchitecture.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AllowAnonymousMiddlewareAttribute : Attribute
    {
    }
}
