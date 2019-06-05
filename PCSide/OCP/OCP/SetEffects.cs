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

namespace OCP
{
    public partial class SetEffects : MetroForm
    {
        public SetEffects()
        {
            InitializeComponent();
        }

        private void ComboBox_effect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBox_effect.Text)
            {
                case "Громкость устройства":
                    var deviceEnum = new MMDeviceEnumerator();
                    var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();

                    foreach (MMDevice device in devices)
                    {
                        ComboBox_audioDevice.Items.Add(device.FriendlyName);
                    }
                    SetTLP(1);
                    break;
                case "Яркость монитора":
                    SetTLP(2);
                    break;
                case "Цветовая гамма":
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
                    }
                    SetTLP(4);
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

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            string gammaNColor = "";
            if (RadioButton_gammaR.Checked)
            {
                gammaNColor = "R";
            }else if (RadioButton_gammaG.Checked)
            {
                gammaNColor = "G";
            }else if(RadioButton_gammaB.Checked)
            {
                gammaNColor = "B";
            }

            XDocument xDoc = XDocument.Load("effects.xml");

            XElement effects = xDoc.Element("effects");
            XElement effect = new XElement("effect");
            XAttribute category = new XAttribute("category", ComboBox_category.Items.IndexOf(ComboBox_category.Text));
            XAttribute position = new XAttribute("position", ComboBox_position.Items.IndexOf(ComboBox_position.Text));
            XAttribute effectName = new XAttribute("effectName", ComboBox_effect.Text);
            XElement audioDeviceID = new XElement("audioDeviceID", ComboBox_audioDevice.Items.IndexOf(ComboBox_audioDevice.Text));
            XElement brtMin = new XElement("brtMin", TextBox_brtMin.Text);
            XElement brtMax = new XElement("brtMax", TextBox_brtMax.Text);
            XElement gammaMin = new XElement("gammaMin", TextBox_gammaMin.Text);
            XElement gammaMax = new XElement("gammaMax", TextBox_gammaMax.Text);
            XElement gammaColor = new XElement("gammaColor", gammaNColor);
            XElement reobasFanID = new XElement("reobasFanID", ComboBox_reobasFan.Text);
            XElement reobasMin = new XElement("reobasMin", TextBox_reobasMin.Text);
            XElement reobasMax = new XElement("reobasMax", TextBox_reobasMax.Text);

            effect.Add(category, position, effectName, audioDeviceID, brtMin, brtMax, gammaMin, gammaMax, gammaColor, reobasFanID, reobasMin, reobasMax);
            effects.Add(effect);

            xDoc.Save("effects.xml");
        }
    }
}
