using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fakeNode
{
    public partial class Form1 : Form
    {
        string devEui;
        string appEui;
        string appKey;

        string devAddr;
        string nwkSKey;
        string appSKey;

        string data;
        string payload;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //Check all the required values are set.
            devEui = txtDevEUI.Text;
            if (devEui.Length == 0)
            {
                MessageBox.Show("Device EUI is required");
                return;
            }
            progressBar1.Value += 10;

            appEui = txtAppEUI.Text;
            if (appEui.Length == 0)
            {
                MessageBox.Show("App EUI is required");
                return;
            }
            progressBar1.Value += 10;

            appKey = txtAppKey.Text;
            if (appKey.Length == 0)
            {
                MessageBox.Show("App Key is required");
                return;
            }
            progressBar1.Value += 10;

            data = txtData.Text;
            if (data.Length == 0)
            {
                MessageBox.Show("Data is required");
                return;
            }
            progressBar1.Value += 10;

            //Convert the data into hex
            byte[] ba = Encoding.Default.GetBytes(data);
            payload = BitConverter.ToString(ba);
            payload = payload.Replace("-", "");

                //Attempt to join
                if(joinOTAA())
                {
                    //Send Message
                    sendMessage();
                    MessageBox.Show("Message Sent");
                    return;
                }
                else
                {
                    MessageBox.Show("Unable to Join OTAA");
                    return;
                }
            

        }

        private bool useApplication()
        {
            bool result = false;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ttnctl.exe";
            startInfo.Arguments = string.Format("applications use {0}", appEui);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();

            while (!process.StandardOutput.EndOfStream)
            {
                string text = process.StandardOutput.ReadLine();

                if(text.Contains("INFO You are now using application"))
                {
                    result = true;
                }
                progressBar1.Value += 10;
            }

            return result;
        }

        private bool joinOTAA()
        {
            bool result = false;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ttnctl.exe";
            //startInfo.Arguments = string.Format("join {0} {1}", devEui, appKey);
            startInfo.Arguments = string.Format("join {0} {1} --app-eui {2}", devEui, appKey, appEui);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();

            while (!process.StandardOutput.EndOfStream)
            {
                int num;
                string text = process.StandardOutput.ReadLine();

                if (text.Contains("INFO Device Address:"))
                {
                    num = text.IndexOf(':') + 2;
                    devAddr = text.Substring(num).Trim();
                    progressBar1.Value += 10;
                }

                if (text.Contains("INFO Network Session Key:"))
                {
                    num = text.IndexOf(':') + 2;
                    nwkSKey = text.Substring(num).Trim();
                    progressBar1.Value += 10;
                }

                if (text.Contains("INFO Application Session Key:"))
                {
                    num = text.IndexOf(':') + 2;
                    appSKey = text.Substring(num).Trim();
                    progressBar1.Value += 10;
                }

                if (text.Contains("INFO Network Joined"))
                {
                    result = true;
                    progressBar1.Value += 10;
                }

            }

            if(result)
            {
                txtDevAddr.Text = devAddr;
                txtNwkSKey.Text = nwkSKey;
                txtAppSKey.Text = appSKey;
            }

            return result;

        }

        private bool sendMessage()
        {
            bool result = false;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ttnctl.exe";
            startInfo.Arguments = string.Format("uplink true {0} {1} {2} {3} 5", devAddr, nwkSKey, appSKey, payload);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();
            progressBar1.Value += 20;
            return result;
        }

        private bool checkLoggedIn()
        {
            bool result = false;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ttnctl.exe";
            startInfo.Arguments = string.Format("user");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();

             while (!process.StandardOutput.EndOfStream)
            {
                string text = process.StandardOutput.ReadLine();

                if (text.Contains("WARN No login found."))
                {
                    result = false;
                }

                if (text.Contains("INFO Logged on as"))
                {
                    result = true;
                }
            }

            return result;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!File.Exists("ttnctl.exe"))
            {
                MessageBox.Show("ttnctl not found.");
                Application.Exit();
            }
        }

    }
}
