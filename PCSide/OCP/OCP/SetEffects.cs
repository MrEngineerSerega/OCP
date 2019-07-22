using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using NAudio.CoreAudioApi;
using OpenHardwareMonitor.Hardware;
using Newtonsoft.Json;

namespace OCP
{
    public partial class SetEffects : MetroForm
    {
        string[] PotEffects = new string[] { "Громкость устройства", "Яркость монитора", "Цветовая гамма", "Реобас" };
        string[] ButtEffects = new string[] { "Mute", "Запуск файла", "Сочетание клавиш"};
        public SetEffects()
        {
            InitializeComponent();
        }

        private void ComboBox_effect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();

            switch (ComboBox_effect.Text)
            {
                case "Громкость устройства":
                    foreach (MMDevice device in devices)
                    {
                        ComboBox_audioDevice.Items.Add(device.FriendlyName);
                    }
                    TextBox_min.Text = "0";
                    TextBox_max.Text = "100";
                    SetTLP(1);
                    break;
                case "Яркость монитора":
                    TextBox_min.Text = "0";
                    TextBox_max.Text = "255";
                    SetTLP(2);
                    break;
                case "Цветовая гамма":
                    TextBox_min.Text = "0";
                    TextBox_max.Text = "255";
                    SetTLP(3);
                    break;
                case "Реобас":
                    Computer c = new Computer();
                    c.GPUEnabled = true;
                    c.CPUEnabled = true;
                    c.FanControllerEnabled = true;
                    c.HDDEnabled = true;
                    c.MainboardEnabled = true;
                    c.RAMEnabled = true;
                    c.Open();
                    foreach (var hardware in c.Hardware)
                    {
                        //hardware.Update();
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Control)
                            {
                                ComboBox_reobasFan.Items.Add(sensor.Identifier.ToString());
                            }
                        }
                        if (hardware.SubHardware.Length > 0)
                        {
                            foreach(var subHard in hardware.SubHardware)
                            {
                                foreach (var sensor in subHard.Sensors)
                                {
                                    if (sensor.SensorType == SensorType.Control)
                                    {
                                        ComboBox_reobasFan.Items.Add(sensor.Identifier.ToString());
                                    }
                                }
                            }
                        }
                    }
                    TextBox_min.Text = "0";
                    TextBox_max.Text = "100";
                    SetTLP(4);
                    break;
                case "Mute":
                    foreach (MMDevice device in devices)
                    {
                        ComboBox_muteAudioDevice.Items.Add(device.FriendlyName);
                    }
                    SetTLP(5);
                    break;
                case "Запуск файла":
                    SetTLP(6);
                    break;
                case "Сочетание клавиш":
                    SetTLP(7);
                    break;
                default:
                    SetTLP(0);
                    break;
            }
        }





        void SetTLP(int page)
        {
            for (int i = 0; i < tableLayoutPanel_effects.ColumnStyles.Count; i++)
            {
                tableLayoutPanel_effects.ColumnStyles[i].Width = 0;
            }
            tableLayoutPanel_effects.ColumnStyles[page].Width = 100;
        }
        void SetTLPM(int page)
        {
            for (int i = 0; i < tableLayoutPanel_BOP.ColumnStyles.Count; i++)
            {
                tableLayoutPanel_BOP.ColumnStyles[i].Width = 0;
            }
            tableLayoutPanel_BOP.ColumnStyles[page].Width = 100;
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            char gammaNColor = ' ';
            if (RadioButton_gammaR.Checked)
            {
                gammaNColor = 'R';
            }else if (RadioButton_gammaG.Checked)
            {
                gammaNColor = 'G';
            }else if(RadioButton_gammaB.Checked)
            {
                gammaNColor = 'B';
            }

            EffectsFile effects = JsonConvert.DeserializeObject<EffectsFile>(File.ReadAllText("effects.json"));

            Effect effect = new Effect(ComboBox_category.SelectedIndex, ComboBox_position.SelectedIndex);
            if (ComboBox_category.SelectedIndex < 2)
            {
                effect.PotEffect = new PotEffect(int.Parse(TextBox_min.Text), int.Parse(TextBox_max.Text));
            }
            else
            {
                effect.ButtEffect = new ButtEffect(ComboBox_Event.SelectedIndex);
            }
            switch (ComboBox_effect.Text)
            {
                case "Громкость устройства":
                    effect.PotEffect.Volume = new Volume(ComboBox_audioDevice.SelectedIndex.ToString());
                    break;
                case "Яркость монитора":
                    effect.PotEffect.Brightness = new Brightness();
                    break;
                case "Цветовая гамма":
                    effect.PotEffect.Gamma = new Gamma(gammaNColor);
                    break;
                case "Реобас":
                    effect.PotEffect.Reobas = new Reobas(ComboBox_reobasFan.Text);
                    break;
                case "Mute":
                    effect.ButtEffect.Mute = new Mute(ComboBox_muteAudioDevice.SelectedIndex.ToString());
                    break;
                case "Запуск файла":
                    effect.ButtEffect.RunFile = new RunFile(TextBox_RunFile.Text);
                    break;
                case "Сочетание клавиш":
                    effect.ButtEffect.KeyboardShortcut = new KeyboardShortcut(TextBox_keybSh.Text);
                    break;
            }

            effects.Effects.Add(effect);
            File.WriteAllText("effects.json", JsonConvert.SerializeObject(effects));
        }

        private void ComboBox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboBox_category.SelectedIndex < 2)
            {
                ComboBox_effect.Items.Clear();
                ComboBox_effect.Items.AddRange(PotEffects);
                SetTLPM(1);
            }
            else
            {
                ComboBox_effect.Items.Clear();
                ComboBox_effect.Items.AddRange(ButtEffects);
                SetTLPM(2);
            }
        }

        private void Button_runBrowse_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox_RunFile.Text = openFileDialog1.FileName;
            }
        }
    }
}
