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
using Newtonsoft.Json;
using System.IO;

namespace OCP
{
    public partial class form1 : MetroForm
    {
        static SerialPort Serial = new SerialPort("COM6", 9600);
        static EffectsFile effects = JsonConvert.DeserializeObject<EffectsFile>(File.ReadAllText("effects.json"));
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
            new Effect().PotEffect.Reobas.ReobasFStart();
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

                foreach(Effect effect in effects.Effects)
                {
                    if(ch == effect.Category.ToString() + effect.Position.ToString())
                    {
                        try
                        {
                            if (effect.Category < 2)
                            {
                                if (effect.PotEffect.Volume != null)
                                {
                                    effect.PotEffect.Volume.SetVolume(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max));
                                }
                                if (effect.PotEffect.Gamma != null)
                                {
                                    switch (effect.PotEffect.Gamma.Color)
                                    {
                                        case 'R':
                                            effect.PotEffect.Gamma.SetRedColor(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max));
                                            break;
                                        case 'G':
                                            effect.PotEffect.Gamma.SetGreenColor(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max));
                                            break;
                                        case 'B':
                                            effect.PotEffect.Gamma.SetBlueColor(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max));
                                            break;
                                        case 'A':
                                            effect.PotEffect.Gamma.SetBrightness(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max)); 
                                            break;
                                    }
                                }
                                if (effect.PotEffect.Reobas != null)
                                {
                                    effect.PotEffect.Reobas.SetFanSpeed(map(int.Parse(val), 0, 100, effect.PotEffect.Min, effect.PotEffect.Max));
                                }
                            }
                            else
                            {
                                if (val[0].ToString() == effect.ButtEffect.EventType.ToString())
                                {
                                    if (effect.ButtEffect.Mute != null)
                                    {
                                        effect.ButtEffect.Mute.ToggleMute();
                                    }
                                    if (effect.ButtEffect.RunFile != null)
                                    {
                                        effect.ButtEffect.RunFile.Run();
                                    }
                                    if (effect.ButtEffect.KeyboardShortcut != null)
                                    {
                                        effect.ButtEffect.KeyboardShortcut.Send();
                                    }
                                    if(effect.ButtEffect.MediaButt != null)
                                    {
                                        effect.ButtEffect.MediaButt.Click();
                                    }
                                    if(effect.ButtEffect.LockWorkStation != null)
                                    {
                                        effect.ButtEffect.LockWorkStation.Lock();
                                    }
                                }
                            }
                        }catch(NullReferenceException) { }
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
