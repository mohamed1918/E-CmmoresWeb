using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSec =120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CacheKey = CreateCacheKey(context.HttpContext.Request);

            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(CacheKey);

            if(cacheValue is not null)
            {
                context.Result = new ContentResult
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var ExecutedContext = await next.Invoke();

            if(ExecutedContext.Result is OkObjectResult result)
            {
                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(result.Value);
                await cacheService.SetAsync(CacheKey, jsonResponse, TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);
            key.Append("?");
            foreach(var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}-{item.Value}&");
            }
            return key.ToString();
        }
    }
}
