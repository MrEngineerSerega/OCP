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

namespace OCP
{
    public partial class Form1 : Form
    {
        static SerialPort Serial = new SerialPort("COM4", 9600);
        Thread thSetPot = new Thread(SetPot);
        static TrackBar[] relativePot = new TrackBar[5];
        public delegate void InvokeDelegate();
        static TextBox txt;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            relativePot = new TrackBar[] { trackBar1, trackBar2, trackBar3, trackBar4, trackBar5 };
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
