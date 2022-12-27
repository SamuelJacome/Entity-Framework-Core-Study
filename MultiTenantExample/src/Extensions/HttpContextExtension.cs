using Microsoft.AspNetCore.Http;

namespace src.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetTenantId(this HttpContext httpContext)
        {
            // domain.com.br/tenant-1/product -> split -> "" / "tenant-1" / "product" lista com 2 elementos

            var tenant = httpContext.Request.Path.Value.Split("/", System.StringSplitOptions.RemoveEmptyEntries)[0];

            return tenant;
        }

    }
}