using System.Collections.Generic;
using System.Text;

namespace ExampleZKPush.Models
{
    public class User : IProtocol
    {
        public int Pin { get; set; }
        public string Name { get; set; }
        public int CardNumber { get; set; }
        public string Password { get; set; }
        public TPrivilege Privilege { get; set; }

        public enum TPrivilege
        {
            CommonUser = 0,
            SuperAdministrator = 14
        }

        public string ToProtocol()
        {
            var protocol = new StringBuilder();

            protocol.AppendFormat("Pin={0}{1}", Pin, '\t');
            protocol.AppendFormat("CardNo={0}{1}", CardNumber, '\t');
            protocol.AppendFormat("Password={0}{1}", Password, '\t');
            protocol.AppendFormat("Name={0}{1}", Name, '\t');
            protocol.AppendFormat("Group={0}{1}", "1", '\t');
            protocol.AppendFormat("Privilege={0}{1}", (int)Privilege, '\t');

            return protocol.ToString();
        }

        public string GetTableName() => "user";
    }
}
