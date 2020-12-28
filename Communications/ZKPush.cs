using ExampleZKPush.Events;
using ExampleZKPush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ExampleZKPush.Communications
{
    public class ZKPush
    {
        #region Communication Control

        public string SerialNumber { get; private set; }
        private DateTime TimeWait { get; set; }
        private ZKPushRequest CommunicationRequest { get; set; }

        private readonly object _lockRequest = new object();

        public event EventHandler<DeviceMessageEventArgs> DeviceMessageEventHandler;

        public ZKPush(string serialNumber)
        {
            SerialNumber = serialNumber;
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            var bytes = new byte[262144]; // 256 kb

            var bytesRead = context.Request.InputStream.Read(bytes, 0, bytes.Length);

            var data = Encoding.ASCII.GetString(bytes, 0, bytesRead);

            var rawUrl = context.Request.RawUrl;

            DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
            {
                SerialNumber = SerialNumber,
                Message = $"{DateTime.Now:HH:mm:ss} - {rawUrl}"
            });

            lock (_lockRequest)
            {
                VerifyRequest(context, rawUrl, data);
            }
        }

        private void VerifyRequest(HttpListenerContext context, string route, string data)
        {
            if (route.Contains("/iclock/registry"))
            {
                var registryCode = DateTime.Now.ToString("MMddHHmmss");

                AnswerRequest(context, $"RegistryCode={registryCode}");
            }
            else if (route.Contains("/iclock/push"))
            {
                var parameters = ConfigureParameters();

                AnswerRequest(context, parameters);
            }
            else if (route.Contains("/iclock/getrequest"))
            {
                if (CommunicationRequest?.StateRequest == ZKPushRequest.TStateRequest.AwaitingTransmission)
                {
                    CommunicationRequest.StateRequest = ZKPushRequest.TStateRequest.AwaitingResponse;

                    AnswerRequest(context, CommunicationRequest.CommandText);
                }
                else
                {
                    AnswerRequest(context, "OK");
                }
            }
            else if (route.Contains("/iclock/ping"))
            {
                TimeWait = DateTime.Now;

                AnswerRequest(context, "OK");
            }
            else if (route.Contains("/iclock/devicecmd"))
            {
                if (CommunicationRequest != null)
                {
                    CommunicationRequest.StateRequest = ZKPushRequest.TStateRequest.Completed;
                    CommunicationRequest.ReponseCommand = data;
                }

                AnswerRequest(context, "OK");
            }
            else if (route.Contains("/iclock/querydata"))
            {
                if (CommunicationRequest != null)
                {
                    if (!string.IsNullOrEmpty(CommunicationRequest.ResponseData))
                    {
                        CommunicationRequest.ResponseData += "\r\n";
                    }

                    CommunicationRequest.ResponseData += data;
                }

                AnswerRequest(context, "OK");
            }
            else
            {
                AnswerRequest(context, "OK");
            }
        }

        private void AnswerRequest(HttpListenerContext context, string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);

            context.Response.Headers.Add("Connection", "close");
            context.Response.ContentType = "text/plain;charset=UTF-8";
            context.Response.ContentLength64 = bytes.Length;
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.OutputStream.Close();
        }

        private void AwaitRequest()
        {
            TimeWait = DateTime.Now;

            while (CommunicationRequest.StateRequest != ZKPushRequest.TStateRequest.Completed &&
                   DateTime.Now.Subtract(TimeWait).TotalSeconds < 10)
            {
                Thread.Sleep(100);
            }

            if (CommunicationRequest.StateRequest != ZKPushRequest.TStateRequest.Completed)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = "REQUEST_TIMEOUT"
                });

                return;
            }

            var listResponse = TreatReceivedResponse(CommunicationRequest.ReponseCommand);

            foreach (var response in listResponse)
            {
                var returnCode = Convert.ToInt32(response["Return"]);

                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"RETURN_CODE: {returnCode}"
                });
            }
        }

        private string ConfigureParameters()
        {
            var pushParameters = new StringBuilder();

            pushParameters.AppendLine("ServerVersion=3.0.1");
            pushParameters.AppendLine("ServerName=ADMS");
            pushParameters.AppendLine("PushVersion=3.0.1");
            pushParameters.AppendLine("ErrorDelay=10");
            pushParameters.AppendLine("RequestDelay=3");
            pushParameters.AppendLine("TransInterval=1");
            pushParameters.AppendLine("TransTables=User Transaction Facev7 templatev10");
            pushParameters.AppendLine("TimeZone=-3");
            pushParameters.AppendLine("RealTime=1");
            pushParameters.AppendLine("TimeoutSec=10");

            return pushParameters.ToString();
        }

        private void SendCommand(string command)
        {
            CommunicationRequest = new ZKPushRequest()
            {
                CommandText = command
            };

            AwaitRequest();
        }

        private void SendCommand(List<IProtocol> listData)
        {
            var listCommands = new List<string>();

            var commandIndex = 0;

            foreach (var data in listData)
            {
                commandIndex++;

                listCommands.Add($"C:{commandIndex}:DATA UPDATE {data.GetTableName()} {data.ToProtocol()}");
            }

            CommunicationRequest = new ZKPushRequest()
            {
                CommandText = string.Join("\r\n", listCommands)
            };

            AwaitRequest();
        }

        private List<Dictionary<string, string>> TreatReceivedResponse(string text)
        {
            var listResponse = new List<Dictionary<string, string>>();

            foreach (var response in text.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var listText = response.Split('&');

                var dictionaryData = CreateResponseDictionary(listText);

                listResponse.Add(dictionaryData);
            }

            return listResponse;
        }

        private Dictionary<string, string> TreatReceivedData(string text)
        {
            var listText = text.Split('\t');

            var dictionaryData = CreateResponseDictionary(listText);

            return dictionaryData;
        }

        private Dictionary<string, string> TreatReceivedConfigurations(string text)
        {
            // Some properties has "~" in the name.
            var listText = text.Replace("~", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var dictionaryData = CreateResponseDictionary(listText);

            return dictionaryData;
        }

        private Dictionary<string, string> CreateResponseDictionary(string[] response)
        {
            var dictionaryData = new Dictionary<string, string>();

            foreach (var item in response)
            {
                var listText = item.Split('=');

                dictionaryData.Add(listText[0], listText[1]);
            }

            return dictionaryData;
        }

        #endregion

        #region Maintenance Date and Time

        public void SendDateAndTime()
        {
            SendCommand($"C:1:SET OPTIONS DateTime={GetDateAndTimeInSeconds()}");
        }

        private int GetDateAndTimeInSeconds()
        {
            var utc = DateTime.UtcNow;

            var result = ((utc.Year - 2000) * 12 * 31 + ((utc.Month - 1) * 31) + utc.Day - 1)
                * (24 * 60 * 60) + (utc.Hour * 60 + utc.Minute) * 60 + utc.Second;

            return result;
        }

        private DateTime GetDateAndTimeFromSeconds(int segs)
        {
            var seconds = segs % 60;

            segs /= 60;

            var minutos = segs % 60;

            segs /= 60;

            var horas = segs % 24;

            segs /= 24;

            var dias = segs % 31 + 1;

            segs /= 31;

            var meses = segs % 12 + 1;

            segs /= 12;

            var anos = segs + 2000;

            return new DateTime(anos, meses, dias, horas, minutos, seconds);
        }

        #endregion

        #region Maintenance Users

        public void SendUsers()
        {
            var listUsers = GetUserList();

            SendCommand(listUsers);

            var listUserExtended = GetUserExtendedList();

            SendCommand(listUserExtended);

            var listUserAuthorize = GetUserAuthorizeList();

            SendCommand(listUserAuthorize);
        }

        public void ReceiveUsers()
        {
            SendCommand("C:1:DATA QUERY tablename=user,fielddesc=*,filter=*");

            var users = CommunicationRequest.ResponseData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var listUsers = new List<User>();

            foreach (var user in users)
            {
                var dicionaryUser = TreatReceivedData(user);

                listUsers.Add(new User()
                {
                    Name = dicionaryUser["name"],
                    Pin = int.Parse(dicionaryUser["pin"]),
                    Password = dicionaryUser["password"],
                    Privilege = (User.TPrivilege)int.Parse(dicionaryUser["privilege"])
                });
            }

            foreach (var user in listUsers)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"USER: PIN - {user.Pin} - NAME - {user.Name}"
                });
            }
        }

        private List<IProtocol> GetUserList()
        {
            var listUsers = new List<IProtocol>();

            foreach (var code in Enumerable.Range(1, 10))
            {
                listUsers.Add(new User()
                {
                    Pin = code,
                    CardNumber = code,
                    Name = $"User {code}",
                    Password = code.ToString(),
                    Privilege = User.TPrivilege.CommonUser
                });
            }

            return listUsers;
        }

        private List<IProtocol> GetUserExtendedList()
        {
            var listUsersExtended = new List<IProtocol>();

            foreach (var code in Enumerable.Range(1, 10))
            {
                listUsersExtended.Add(new UserExtended()
                {
                    Pin = code,
                    FirstName = $"User {code}",
                });
            }

            return listUsersExtended;
        }

        private List<IProtocol> GetUserAuthorizeList()
        {
            var listUsersAuthorize = new List<IProtocol>();

            foreach (var code in Enumerable.Range(1, 10))
            {
                listUsersAuthorize.Add(new UserAuthorize()
                {
                    Pin = code
                });
            }

            return listUsersAuthorize;
        }

        #endregion

        #region Maintenance Transactions

        public void ReceiveTransactions()
        {
            SendCommand("C:1:DATA QUERY tablename=transaction,fielddesc=*,filter=*");

            var transactions = CommunicationRequest.ResponseData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var listTransactions = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                var listData = transaction.Split('\t');

                var transactionDictionary = CreateResponseDictionary(listData);

                listTransactions.Add(new Transaction()
                {
                    Pin = int.Parse(transactionDictionary["pin"]),
                    CardNumber = int.Parse(transactionDictionary["transaction cardno"]),
                    EventType = int.Parse(transactionDictionary["eventtype"]),
                    InOutState = int.Parse(transactionDictionary["inoutstate"]),
                    DoorId = int.Parse(transactionDictionary["doorid"]),
                    Verified = int.Parse(transactionDictionary["verified"]),
                    DateAndTime = GetDateAndTimeFromSeconds(int.Parse(transactionDictionary["time_second"]))
                });
            }

            foreach (var transaction in listTransactions)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"TRANSACTION: PIN - {transaction.Pin} - DATETIME - {transaction.DateAndTime}"
                });
            }
        }

        #endregion

        #region Maintenance Biometrics

        public void ReceiveTemplates()
        {
            SendCommand("C:1:DATA QUERY tablename=templatev10,fielddesc=*,filter=*");

            var templates = CommunicationRequest.ResponseData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var listTemplates = new List<Templatev10>();

            foreach (var template in templates)
            {
                var listData = template.Split('\t');

                var templateDictionary = CreateResponseDictionary(listData);

                listTemplates.Add(new Templatev10()
                {
                    Pin = int.Parse(templateDictionary["pin"]),
                    FingerId = int.Parse(templateDictionary["fingerid"]),
                    Valid = int.Parse(templateDictionary["valid"]),
                    Template = templateDictionary["template"]
                });
            }

            foreach (var template in listTemplates)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"TEMPLATEV10: PIN - {template.Pin} - FINGER_ID - {template.FingerId}"
                });
            }
        }

        public void ReceiveBiophotos()
        {
            SendCommand("C:1:DATA QUERY tablename=biophoto,fielddesc=*,filter=*");

            var biophotos = CommunicationRequest.ResponseData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var listBiophotos = new List<Biophoto>();

            foreach (var biophoto in biophotos)
            {
                var listData = biophoto.Split('\t');

                var templateDictionary = CreateResponseDictionary(listData);

                listBiophotos.Add(new Biophoto()
                {
                    Pin = int.Parse(templateDictionary["biophoto pin"]),
                    Size = int.Parse(templateDictionary["size"]),
                    FileName = templateDictionary["filename"],
                    Content = templateDictionary["content"],
                });
            }

            foreach (var biophoto in listBiophotos)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"BIOPHOTO: PIN - {biophoto.Pin} - FILE_NAME - {biophoto.FileName}"
                });
            }
        }

        #endregion

        #region Maintenance Options

        public void GetOptions()
        {
            SendCommand("C:1:GET OPTIONS ~DeviceName,FirmVer,IPAddress,NetMask,GATEIPAddress");

            var configurationDictionary = TreatReceivedConfigurations(CommunicationRequest.ResponseData);

            foreach (var configuration in configurationDictionary)
            {
                DeviceMessageEventHandler?.Invoke(this, new DeviceMessageEventArgs()
                {
                    SerialNumber = SerialNumber,
                    Message = $"OPTIONS: {configuration.Key} - {configuration.Value}"
                });
            }
        }

        #endregion
    }
}
