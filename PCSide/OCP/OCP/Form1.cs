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
using System.Diagnostics;

namespace OCP
{
    public partial class form1 : MetroForm
    {
        static SerialPort Serial = new SerialPort("COM5", 9600);
        static XDocument xDoc = XDocument.Load("effects.xml");
        static Effects Effects = new Effects();
        Thread thSetPot = new Thread(SetPot);
        static MetroProgressSpinner[] relativePot0;
        static Button[] relativeButt;
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
            Effects.ReobasFStart();
            relativePot0 = new MetroProgressSpinner[] { metroProgressSpinner1, metroProgressSpinner2, metroProgressSpinner3, metroProgressSpinner4, metroProgressSpinner5, metroProgressSpinner6, metroProgressSpinner7, metroProgressSpinner7 };
            relativeButt = new Button[] { button2, button3, button4, button5, button6, button7, button8, button9 };
            thSetPot.Start();
        }

        static void SetPot()
        {
            Serial.Open();
            while (true)
            {
                string data = Serial.ReadLine();
                string ch = data.Split(':')[0];
                string val = data.Split(':')[1];

                if (ch[0] == '0') {
                    relativePot0[int.Parse(ch[1].ToString())].BeginInvoke(new MethodInvoker(delegate { relativePot0[int.Parse(ch[1].ToString())].Value = int.Parse(val); }));
                }else if(ch[0] == '2' && val[0] == '0')
                {
                    if(relativeButt[int.Parse(ch[1].ToString())].BackColor == Color.DimGray)
                    {
                        relativeButt[int.Parse(ch[1].ToString())].BeginInvoke(new MethodInvoker(delegate { relativeButt[int.Parse(ch[1].ToString())].BackColor = Color.DarkGreen; }));
                    }
                    else
                    {
                        relativeButt[int.Parse(ch[1].ToString())].BeginInvoke(new MethodInvoker(delegate { relativeButt[int.Parse(ch[1].ToString())].BackColor = Color.DimGray; }));
                    }
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
                            case "Цветовая гамма":
                                XElement gammaMin = effect.Element("gammaMin");
                                XElement gammaMax = effect.Element("gammaMax");
                                XElement gammaColor = effect.Element("gammaColor");
                                switch (gammaColor.Value)
                                {
                                    case "R":
                                        Effects.SetRedColor(map(int.Parse(val), 0, 100, int.Parse(gammaMin.Value), int.Parse(gammaMax.Value)));
                                        break;
                                    case "G":
                                        Effects.SetGreenColor(map(int.Parse(val), 0, 100, int.Parse(gammaMin.Value), int.Parse(gammaMax.Value)));
                                        break;
                                    case "B":
                                        Effects.SetBlueColor(map(int.Parse(val), 0, 100, int.Parse(gammaMin.Value), int.Parse(gammaMax.Value)));
                                        break;
                                }
                                break;
                            case "Реобас":
                                XElement reobasFanID = effect.Element("reobasFanID");
                                XElement reobasMin = effect.Element("reobasMin");
                                XElement reobasMax = effect.Element("reobasMax");
                                Effects.SetFanSpeed(map(int.Parse(val), 0, 100, int.Parse(reobasMin.Value), int.Parse(reobasMax.Value)));
                                break;
                            case "Mute":
                                XElement muteAudioDeviceID = effect.Element("muteAudioDeviceID");
                                XElement muteEvent = effect.Element("muteEvent");
                                if(val[0] == muteEvent.Value[0])
                                {
                                    Effects.Mute(int.Parse(muteAudioDeviceID.Value));
                                }
                                break;
                            case "Запуск файла":
                                XElement runFile = effect.Element("runFile");
                                XElement runFileEvent = effect.Element("runFileEvent");
                                if (val[0] == runFileEvent.Value[0])
                                {
                                    Process.Start(runFile.Value);
                                }
                                break;
                            case "Сочетание клавиш":
                                XElement keybSh = effect.Element("keybSh");
                                XElement keybEvent = effect.Element("keybEvent");
                                if (val[0] == keybEvent.Value[0])
                                {
                                    SendKeys.SendWait(keybSh.Value);
                                }
                                break;
                        }
                    }
                }
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
