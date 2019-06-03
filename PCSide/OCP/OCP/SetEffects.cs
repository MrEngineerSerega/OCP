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
            if (ComboBox_effect.Text == "Громкость устройства")
            {
                var deviceEnum = new MMDeviceEnumerator();
                var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();

                foreach (MMDevice device in devices)
                {
                    ComboBox_audioDevice.Items.Add(device.FriendlyName);
                }
                SetTLP(1);
            }
            else
            {
                SetTLP(0);
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
            XDocument xDoc = XDocument.Load("effects.xml");

            XElement effects = xDoc.Element("effects");
            XElement effect = new XElement("effect");
            XAttribute category = new XAttribute("category", ComboBox_category.Items.IndexOf(ComboBox_category.Text));
            XAttribute position = new XAttribute("position", ComboBox_position.Items.IndexOf(ComboBox_position.Text));
            XAttribute effectName = new XAttribute("effectName", ComboBox_effect.Text);
            XElement audioDeviceID = new XElement("audioDeviceID", ComboBox_audioDevice.Items.IndexOf(ComboBox_audioDevice.Text));
            XElement K = new XElement("K");

            effect.Add(category, position, effectName, audioDeviceID, K);
            effects.Add(effect);

            xDoc.Save("effects.xml");

            Effects Effects = new Effects();
        }
    }
}
