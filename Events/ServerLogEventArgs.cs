using System;

namespace ExampleZKPush.Events
{
    public class ServerLogEventArgs : EventArgs
    {
        public string Event { get; set; }
    }
}
