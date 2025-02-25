using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.Services.CacheServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.APIs.Attributes;

public class CachedAttribute(int timeLifeInSeconds) : Attribute, IAsyncActionFilter
{
    private readonly int _timeLifeInSeconds = timeLifeInSeconds;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var _cacheResponseService = context.HttpContext.RequestServices.GetRequiredService<ICacheResponseService>();

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

        var cacheResponse = await _cacheResponseService.GetObjectInMemoryAsync(cacheKey);

        if (!string.IsNullOrEmpty(cacheResponse)) 
        {
            var contentResult = new ContentResult
            {
                Content = cacheResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = contentResult;
            return;
        }

        var executedContext = await next.Invoke();
        if (executedContext.Result is OkObjectResult executedContextResult && executedContextResult.Value is not null)
            await _cacheResponseService.SetObjectInMemoryAsync(cacheKey, executedContextResult.Value, TimeSpan.FromSeconds(_timeLifeInSeconds));
    }

    private string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var builder = new StringBuilder();

        builder.Append($"{request.Path}");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            builder.Append($"|{key}-{value}");
        }

        return builder.ToString();

    }
}
