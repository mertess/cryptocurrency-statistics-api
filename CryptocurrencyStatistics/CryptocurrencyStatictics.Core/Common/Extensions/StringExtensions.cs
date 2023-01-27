using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatictics.Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static T FromJson<T>(this string self)
        {
            if (string.IsNullOrEmpty(self)) 
                return default;

            return JsonConvert.DeserializeObject<T>(self);
        }

        public static object FromJson(this string self, Type type)
        {
            if (string.IsNullOrEmpty(self))
                return default;

            return JsonConvert.DeserializeObject(self, type);
        }
    }
}
