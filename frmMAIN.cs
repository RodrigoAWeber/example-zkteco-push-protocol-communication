using ExampleZKPush.Events;
using ExampleZKPush.Communications;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleZKPush
{
    public partial class frmMAIN : Form
    {
        public frmMAIN()
        {
            InitializeComponent();
        }

        private readonly ZKPushManager zkPushManager = new ZKPushManager();

        private void btnSTART_Click(object sender, EventArgs e)
        {
            zkPushManager.ServerLogEventHandler += OnServerLogEventHandler;
            zkPushManager.DeviceMessageEventHandler += OnDeviceEventsEventHandler;
            zkPushManager.DeviceConnectedEventHandler += OnDeviceConnectedEventHandler;

            zkPushManager.Start();
        }

        private void btnSTOP_Click(object sender, EventArgs e)
        {
            zkPushManager.Stop();

            zkPushManager.ServerLogEventHandler -= OnServerLogEventHandler;
            zkPushManager.DeviceMessageEventHandler -= OnDeviceEventsEventHandler;
            zkPushManager.DeviceConnectedEventHandler -= OnDeviceConnectedEventHandler;
        }

        private void OnDeviceConnectedEventHandler(object sender, DeviceEventArgs e)
        {
            Invoke(new Action(() =>
            {
                lstDEVICES.Items.Add(e.SerialNumber);
            }));
        }

        private void OnServerLogEventHandler(object sender, ServerLogEventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtEVENTS.AppendText($"{e.Event}" + "\r\n");
            }));
        }

        private void OnDeviceEventsEventHandler(object sender, DeviceMessageEventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtEVENTS.AppendText($"{e.SerialNumber} - {e.Message}" + "\r\n");
            }));
        }

        private void btnSEND_DATE_AND_TIME_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.SendDateAndTime);
            }
        }

        private void btnSEND_USERS_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.SendUsers);
            }
        }

        private void btnRECEIVE_USERS_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.ReceiveUsers);
            }
        }

        private void btnRECEIVE_TEMPLATES_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.ReceiveTemplates);
            }
        }

        private void btnRECEIVE_TRANSACTIONS_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.ReceiveTransactions);
            }
        }

        private void btnGET_OPTIONS_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.GetOptions);
            }
        }

        private void btnRECEIVE_BIOPHOTOS_Click(object sender, EventArgs e)
        {
            foreach (var zkPush in zkPushManager.ControlledZks)
            {
                Task.Run(zkPush.Value.ReceiveBiophotos);
            }
        }
    }
}
