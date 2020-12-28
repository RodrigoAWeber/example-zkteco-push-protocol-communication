using System;

namespace ExampleZKPush.Models
{
    public class Transaction
    {
        public int CardNumber { get; set; }
        public int Pin { get; set; }
        public int Verified { get; set; }
        public int DoorId { get; set; }
        public int EventType { get; set; }
        public int InOutState { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}
