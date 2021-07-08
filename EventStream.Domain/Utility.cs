using System;
using System.Text;
using Newtonsoft.Json;

namespace EventStream.Domain
{
    public static class Utility
    {
        public static byte[] Encode(this object data) =>
            Encoding.ASCII.GetBytes(data.ToString() ?? throw new InvalidOperationException());

        public static T Decode<T>(this byte[] data) => JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data, 1, data.Length - 1));
    }
}