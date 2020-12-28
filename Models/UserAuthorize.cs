using System.Text;

namespace ExampleZKPush.Models
{
    public class UserAuthorize : IProtocol
    {
        public int Pin { get; set; }

        public string ToProtocol()
        {
            var protocol = new StringBuilder();

            protocol.AppendFormat("Pin={0}{1}", Pin, '\t');
            protocol.AppendFormat("AuthorizeTimezoneId={0}{1}", "1", '\t'); // Default Timezone
            protocol.AppendFormat("AuthorizeDoorId={0}{1}", "15", '\t');    // Granted in all ports

            return protocol.ToString();
        }

        public string GetTableName() => "userauthorize";
    }
}
