using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Xml.Linq;

namespace OCP
{
    public partial class form1 : MetroForm
    {
        static SerialPort Serial = new SerialPort("COM4", 9600);
        static XDocument xDoc = XDocument.Load("effects.xml");
        static Effects Effects = new Effects();
        Thread thSetPot = new Thread(SetPot);
        static MetroProgressSpinner[] relativePot0 = new MetroProgressSpinner[8];
        public delegate void InvokeDelegate();

        static int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }


        public form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            relativePot0 = new MetroProgressSpinner[] { metroProgressSpinner1, metroProgressSpinner2, metroProgressSpinner3, metroProgressSpinner4, metroProgressSpinner5, metroProgressSpinner6, metroProgressSpinner7, metroProgressSpinner7 };
            thSetPot.Start();
        }

        static void SetPot()
        {
            Serial.Open();
            while (true)
            {
                try
                {
                    string data = Serial.ReadLine();
                    string ch = data.Split(':')[0];
                    string val = data.Split(':')[1];

                    if (ch[0] == '0') {
                        relativePot0[int.Parse(ch[1].ToString())].BeginInvoke(new MethodInvoker(delegate { relativePot0[int.Parse(ch[1].ToString())].Value = int.Parse(val); }));
                    }

                    foreach (XElement effect in xDoc.Element("effects").Elements("effect"))
                    {
                        XAttribute category = effect.Attribute("category");
                        XAttribute position = effect.Attribute("position");
                        XAttribute effectName = effect.Attribute("effectName");

                        if (ch == category.Value + position.Value)
                        {
                            switch (effectName.Value)
                            {
                                case "Громкость устройства":
                                    XElement audioDeviceID = effect.Element("audioDeviceID");
                                    Effects.SetVolumeDevice(int.Parse(audioDeviceID.Value), int.Parse(val));
                                    break;
                                case "Яркость монитора":
                                    XElement brtMin = effect.Element("brtMin");
                                    XElement brtMax = effect.Element("brtMax");
                                    Effects.SetBrightness(map(int.Parse(val), 0, 100, int.Parse(brtMin.Value), int.Parse(brtMax.Value)));
                                    break;
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thSetPot.Abort();
        }

        private void Btn_SetEffects_Click(object sender, EventArgs e)
        {
            SetEffects setEffects = new SetEffects();
            setEffects.Show();
        }
    }
}
