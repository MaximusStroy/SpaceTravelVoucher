namespace SpaceTravelVoucher.API.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _config;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(AutnConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key missing (Секретный ключ к API не указан)");
                return;
            }

            var apiKey = _config.GetValue<string>(AutnConstants.ApiKeySectionName);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key invalid (Секретного ключа к API не существует)");
                return; 
            }

            await _next(context);
        }
    }
}
