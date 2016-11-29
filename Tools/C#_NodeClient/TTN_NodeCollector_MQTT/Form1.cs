using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Internal;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Session;
using uPLibrary.Networking.M2Mqtt.Utility;

namespace TTN_NodeCollector_MQTT
{
    public partial class Form1 : Form
    {
        MqttClient client;

        string appEui;
        string deviceEui;
        string appAccessKey;

        public Form1()
        {
            InitializeComponent();
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.Text += value + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appEui = txtAppEUI.Text;
            deviceEui = txtDevEUI.Text;
            appAccessKey = txtAppAccessKey.Text;

            if (appEui.Length == 0)
            {
                MessageBox.Show("AppEUI is required");
                return;
            }

            if (deviceEui.Length == 0)
            {
                MessageBox.Show("DeviceEUI is required");
                return;
            }

            if (appAccessKey.Length == 0)
            {
                MessageBox.Show("AppAccessKey is required");
                return;
            }

            connect();

        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Read the json and convert it into a .net array
            JObject jsonArray = JObject.Parse(Encoding.UTF8.GetString(e.Message));
            //Extract the data value
            string data = (string)jsonArray["payload"];
            //Decode Base64
            data = Encoding.Default.GetString(Convert.FromBase64String(data));
            //Extract the servertime
            string time = (string)jsonArray["metadata"][0]["server_time"];
            //DateTime convertedDate = DateTime.Parse(time);
            //time = convertedDate.ToString();

            //string message = time + ": " + data;
            //Add the data to the textbox
            //AppendTextBox(message);
            AppendTextBox(data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disconnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create client instance 
            client = new MqttClient("10.25.2.62");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnect();
        } 

        private void connect()
        {
            label1.BackColor = Color.Green;
            label1.Text = "Connected";
            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, appEui, appAccessKey);

            client.Subscribe(new string[] { appEui + "/devices/" + deviceEui + "/up" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            button1.Enabled = false;
            txtDevEUI.Enabled = false;
            txtAppEUI.Enabled = false;
            txtAppAccessKey.Enabled = false;
        }

        private void disconnect()
        {
            if (client.IsConnected)
            {
                client.Disconnect();
            }
            label1.BackColor = Color.Red;
            label1.Text = "Disconnected";
            button1.Enabled = true;
            txtDevEUI.Enabled = true;
            txtAppEUI.Enabled = true;
            txtAppAccessKey.Enabled = true;
        }
    }
}
