namespace ExampleZKPush.Communications
{
    public class ZKPushRequest
    {
        public string CommandText { get; set; }
        public string ReponseCommand { get; set; }
        public string ResponseData { get; set; }
        public TStateRequest StateRequest { get; set; }

        public enum TStateRequest
        {   
            AwaitingTransmission = 0,
            AwaitingResponse = 1,
            Completed = 2
        }
    }
}
