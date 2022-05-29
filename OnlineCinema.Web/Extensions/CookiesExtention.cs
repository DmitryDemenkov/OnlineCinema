using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace OnlineCinema.Web.Extensions
{
    public static class CookiesExtention
    {
        public static void Append<T> (this IResponseCookies cookies, string key, T value)
        {
            cookies.Append(key, JsonSerializer.Serialize(value));
        }

        public static bool TryGetValue<T>(this IRequestCookieCollection cookies, string key, out T value)
        {
            if (cookies.TryGetValue(key, out string stringValue))
            {
                value = JsonSerializer.Deserialize<T>(stringValue);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
    }
}
