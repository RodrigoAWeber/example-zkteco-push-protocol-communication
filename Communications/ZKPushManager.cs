using ExampleZKPush.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleZKPush.Communications
{
    public class ZKPushManager
    {
        public Dictionary<string, ZKPush> ControlledZks { get; private set; } = new Dictionary<string, ZKPush>();

        private bool _stopListener;
        private Thread _threadServidor;
        private HttpListener _httpListener;
        private readonly object _lockRequest = new object();        

        public event EventHandler<ServerLogEventArgs> ServerLogEventHandler;
        public event EventHandler<DeviceEventArgs> DeviceConnectedEventHandler;
        public event EventHandler<DeviceMessageEventArgs> DeviceMessageEventHandler;

        public void Start()
        {
            _stopListener = false;

            _threadServidor = new Thread(StartServerAsync);
            _threadServidor.Name = "ZKPushManager";
            _threadServidor.Start();
        }

        public void Stop()
        {
            _stopListener = true;

            try
            {
                if (_httpListener.IsListening)
                {
                    _httpListener.Stop();
                }

                if (!_threadServidor.Join(TimeSpan.FromSeconds(5)))
                {
                    _threadServidor.Abort();
                }

                ServerLogEventHandler?.Invoke(this, new ServerLogEventArgs()
                {
                    Event = "STOP_SERVER"
                });
            }
            catch (Exception ex)
            {
                ServerLogEventHandler?.Invoke(this, new ServerLogEventArgs()
                {
                    Event = $"STOP_SERVER: {ex.Message}"
                });
            }
        }

        private async void StartServerAsync()
        {
            try
            {
                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add("http://*:8083/");
                _httpListener.Start();

                ServerLogEventHandler?.Invoke(this, new ServerLogEventArgs()
                {
                    Event = "START_SERVER"
                });

                await ListenAsync();
            }
            catch (Exception ex)
            {
                ServerLogEventHandler?.Invoke(this, new ServerLogEventArgs()
                {
                    Event = $"START_SERVER: {ex.Message}"
                });
            }
        }

        private async Task ListenAsync()
        {
            while (!_stopListener)
            {
                try
                {
                    var context = await _httpListener.GetContextAsync();

                    var serialNumber = context.Request.QueryString["SN"];

                    if (string.IsNullOrWhiteSpace(serialNumber))
                    {
                        return;
                    }

                    lock (_lockRequest)
                    {
                        if (!ControlledZks.ContainsKey(serialNumber))
                        {
                            var newZkPush = new ZKPush(serialNumber);

                            newZkPush.DeviceMessageEventHandler +=
                                (sender, e) => DeviceMessageEventHandler?.Invoke(sender, e);

                            ControlledZks.Add(serialNumber, newZkPush);

                            DeviceConnectedEventHandler?.Invoke(this, new DeviceEventArgs()
                            {
                                SerialNumber = serialNumber
                            });
                        }

                        var zkPush = ControlledZks[serialNumber];

                        Task.Run(() => zkPush.ProcessRequest(context));
                    }
                }
                catch (HttpListenerException ex) when (ex.ErrorCode == 995)
                {
                    return; // Error 995 occurs when we are stopping the server
                }
                catch (Exception ex)
                {
                    ServerLogEventHandler?.Invoke(this, new ServerLogEventArgs()
                    {
                        Event = $"LISTEN: {ex.Message}"
                    });
                }
            }
        }
    }
}
