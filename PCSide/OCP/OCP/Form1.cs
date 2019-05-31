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

namespace OCP
{
    public partial class form1 : MetroForm
    {
        static SerialPort Serial = new SerialPort("COM4", 9600);
        Thread thSetPot = new Thread(SetPot);
        static MetroProgressSpinner[] relativePot = new MetroProgressSpinner[8];
        public delegate void InvokeDelegate();

        public form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            relativePot = new MetroProgressSpinner[] { metroProgressSpinner1, metroProgressSpinner2, metroProgressSpinner3, metroProgressSpinner4, metroProgressSpinner5, metroProgressSpinner6, metroProgressSpinner7, metroProgressSpinner7 };
        }

        private void Button1_Click(object sender, EventArgs e)
        {
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

                    relativePot[int.Parse(ch[1].ToString())].BeginInvoke(new MethodInvoker(delegate { relativePot[int.Parse(ch[1].ToString())].Value = int.Parse(val); }));
                }
                catch (Exception) { }
            }
        }
    }
}
