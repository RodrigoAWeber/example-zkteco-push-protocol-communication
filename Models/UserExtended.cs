using System.Text;

namespace ExampleZKPush.Models
{
    public class UserExtended : IProtocol
    {
        public int Pin { get; set; }
        public string FirstName { get; set; }

        public string ToProtocol()
        {
            var protocol = new StringBuilder();

            protocol.AppendFormat("Pin={0}{1}", Pin, '\t');
            protocol.AppendFormat("FirstName={0}{1}", FirstName, '\t');

            return protocol.ToString();
        }

        public string GetTableName() => "extuser";
    }
}
