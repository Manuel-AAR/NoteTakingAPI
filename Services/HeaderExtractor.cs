
namespace backend.Services;

public class HeaderExtractor(HttpContext context)
{
    public HttpContext httpContext = context;

    public string ExtractHeaders (string headerName)
    {
        httpContext.Request.Headers.TryGetValue(headerName, out var headerTemp);
        string headerString = headerTemp.ToString();
        if (string.IsNullOrWhiteSpace(headerString)) throw new Exception("Accessing inexistent header: " + headerName);
        return headerString;
    }
}