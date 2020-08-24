using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDashboardServer.Shared
{
    public class Response<T>
    {
        public bool Successful { get; set; }
        public T Model { get; set; }
        public string Message { get; set; }
    }
    public class MessageItem
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class InvalidResponse
    {
        public List<MessageItem> Message { get; set; }
    }
}
