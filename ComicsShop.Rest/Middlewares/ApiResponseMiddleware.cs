using System.Text.Json;

namespace ComicsShop.Rest.Middlewares;

public class ApiResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ApiResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var currentBody = context.Response.Body;

        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            await _next(context);

            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            var payload = await new StreamReader(memoryStream).ReadToEndAsync();

            if (payload.Length == 0)
            {
                return;
            }

            var result = ApiResponse.Create(context.Response.StatusCode, payload);
            var newResponse = JsonSerializer.SerializeToUtf8Bytes(result);

            await context.Response.Body.WriteAsync(newResponse);
        }
    }
}

public interface IApiResponse
{
    bool success { get; set; }
    object? data { get; set; }
    string? error { get; set; }
}

public static class ApiResponse
{
    public static IApiResponse Create(int statusCode, string payload)
    {
        if (statusCode is >= 200 and <= 399)
        {
            var objResult = JsonSerializer.Deserialize<object>(payload);
            return new SuccessApiResponse(objResult);
        }

        return new ErrorApiResponse(payload);
    }
}

public class SuccessApiResponse : IApiResponse
{
    public bool success { get; set; }
    public object? data { get; set; }
    public string? error { get; set; }

    public SuccessApiResponse(object? payload = null)
    {
        success = true;
        data = payload;
        error = null;
    }
}

public class ErrorApiResponse : IApiResponse
{
    public bool success { get; set; }
    public object? data { get; set; }
    public string error { get; set; }

    public ErrorApiResponse(string errorMessage = "Internal error")
    {
        success = false;
        data = null;
        error = errorMessage.Replace("\"", "");
    }
}

public static class ApiResponseMiddlewareExtensions
{
    public static IApplicationBuilder UseApiResponse(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiResponseMiddleware>();
    }
}