using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using OpenHardwareMonitor.Hardware;

namespace OCP
{
    class Effects
    {
        Computer c = new Computer();

        public int redColor = 128, greenColor = 128, blueColor = 128, allColor = 0;
        const int KEYEVENTF_EXTENDEDKEY = 1;
        const int KEYEVENTF_KEYUP = 2;

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Blue;
        }
        public void SetVolumeDevice(int deviceID, int value)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
            devices[deviceID].AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.00f;
        }
        public void Mute(int deviceID)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
            devices[deviceID].AudioEndpointVolume.Mute = !devices[deviceID].AudioEndpointVolume.Mute;
        }
        public void SetBrightness(int brightness)
        {
            allColor = brightness;
            IntPtr DC = GetDC(GetDesktopWindow());
            RAMP _Rp = new RAMP();

            GetDeviceGammaRamp(DC, ref _Rp);
            for (int i = 0; i < 256; i++)
            {
                int valueR = i * (brightness + redColor);
                int valueG = i * (brightness + greenColor);
                int valueB = i * (brightness + blueColor);

                if (valueR + redColor > 65535)
                    valueR = 65535 - redColor;
                if (valueG + greenColor > 65535)
                    valueG = 65535 - greenColor;
                if (valueB + blueColor > 65535)
                    valueB = 65535 - blueColor;

                _Rp.Red[i] = Convert.ToUInt16(valueR);
                _Rp.Green[i] = Convert.ToUInt16(valueG);
                _Rp.Blue[i] = Convert.ToUInt16(valueB);
            }

            SetDeviceGammaRamp(DC, ref _Rp);
        }
        public void SetRedColor(int brightness)
        {
            redColor = brightness;
            IntPtr DC = GetDC(GetDesktopWindow());
            RAMP _Rp = new RAMP();

            GetDeviceGammaRamp(DC, ref _Rp);
            for (int i = 0; i < 256; i++)
            {
                int value = i * (brightness + allColor);

                if (value > 65535)
                    value = 65535;

                _Rp.Red[i] = Convert.ToUInt16(value);
            }

            SetDeviceGammaRamp(DC, ref _Rp);
        }
        public void SetGreenColor(int brightness)
        {
            greenColor = brightness;
            IntPtr DC = GetDC(GetDesktopWindow());
            RAMP _Rp = new RAMP();

            GetDeviceGammaRamp(DC, ref _Rp);
            for (int i = 0; i < 256; i++)
            {
                int value = i * (brightness + allColor);

                if (value > 65535)
                    value = 65535;

                _Rp.Green[i] = Convert.ToUInt16(value);
            }

            SetDeviceGammaRamp(DC, ref _Rp);
        }
        public void SetBlueColor(int brightness)
        {
            blueColor = brightness;
            IntPtr DC = GetDC(GetDesktopWindow());
            RAMP _Rp = new RAMP();

            GetDeviceGammaRamp(DC, ref _Rp);
            for (int i = 0; i < 256; i++)
            {
                int value = i * (brightness + allColor);

                if (value > 65535)
                    value = 65535;

                _Rp.Blue[i] = Convert.ToUInt16(value);
            }

            SetDeviceGammaRamp(DC, ref _Rp);
        }
        public void ReobasFStart()
        {
            c.GPUEnabled = true;
            c.CPUEnabled = true;
            c.FanControllerEnabled = true;
            c.HDDEnabled = true;
            c.MainboardEnabled = true;
            c.RAMEnabled = true;
            c.Open();
        }
        public void SetFanSpeed(int speed, string id)
        {
            foreach (var hardware in c.Hardware)
            {
                //hardware.Update();
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Control)
                    {
                        sensor.Control.SetSoftware(speed);
                    }
                }
                foreach (var subHard in hardware.SubHardware)
                {
                    foreach (var sensor in subHard.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Control)
                        {
                            if (sensor.Identifier.ToString() == id)
                            {
                                sensor.Control.SetSoftware(speed);
                            }
                        }
                    }
                }

            }
        }
        public static void KeyDown(Keys vKey)
    {
        keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
    }

    public static void KeyUp(Keys vKey)
    {
        keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
    }
    }
}
