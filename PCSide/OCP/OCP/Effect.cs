using NAudio.CoreAudioApi;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCP
{
    class EffectsFile
    {
        public List<Effect> Effects = new List<Effect>();
    }
    class Effect
    {
        public Effect() { }
        public Effect(int category, int position) { Category = category; Position = position; }

        public int Category { get; set; }
        public int Position { get; set; }
        public PotEffect PotEffect { get; set; }
        public ButtEffect ButtEffect { get; set; }
    }
    class PotEffect
    {
        public PotEffect() { }
        public PotEffect(int min, int max) { Min = min; Max = max; }

        public int Min { get; set; }
        public int Max { get; set; }
        public Volume Volume { get; set; }
        public Gamma Gamma { get; set; }
        public Reobas Reobas { get; set; }
    }
    class ButtEffect
    {
        public ButtEffect() { }
        public ButtEffect(int eventType) { EventType = eventType; }

        public int EventType { get; set; }
        public Mute Mute { get; set; }
        public RunFile RunFile { get; set; }
        public KeyboardShortcut KeyboardShortcut { get; set; }
        public MediaButt MediaButt { get; set; }
        public LockWorkStation LockWorkStation { get; set; }
    }

    class Volume 
    {
        public Volume() { }
        public Volume(string audioDeviceID) { AudioDeviceID = audioDeviceID; }

        public string AudioDeviceID { get; set; }
        public void SetVolume(int value)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
            devices[int.Parse(AudioDeviceID)].AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.00f;
        }
    }
    class Gamma
    {
        public Gamma() { }
        public Gamma(char color) { Color = color; }

        public char Color { get; set; }

        public int redColor = 128, greenColor = 128, blueColor = 128, allColor = 0;
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
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
    }
    class Reobas
    {
        public Reobas() { }
        public Reobas(string fanID) { FanID = fanID; }

        public string FanID { get; set; }

        Computer c = new Computer();
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
        public void SetFanSpeed(int speed)
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
                            if (sensor.Identifier.ToString() == FanID)
                            {
                                sensor.Control.SetSoftware(speed);
                            }
                        }
                    }
                }

            }
        }
    }

    class Mute
    {
        public Mute() { }
        public Mute(string audioDeviceID) { AudioDeviceID = audioDeviceID; }

        public string AudioDeviceID { get; set; }
        public void ToggleMute()
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
            devices[int.Parse(AudioDeviceID)].AudioEndpointVolume.Mute = !devices[int.Parse(AudioDeviceID)].AudioEndpointVolume.Mute;
        }
    }
    class RunFile
    {
        public RunFile() { }
        public RunFile(string file, string param) { File = file; Params = param; }

        public string File { get; set; }
        public string Params { get; set; }
        public void Run()
        {
            Process.Start(File, Params);
        }
    }
    class KeyboardShortcut
    {
        public KeyboardShortcut() { }
        public KeyboardShortcut(string shortcut) {Shortcut = shortcut; }

        public string Shortcut { get; set; }
        public void Send()
        {
            SendKeys.SendWait(Shortcut);
        }
    }
    class MediaButt
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public MediaButt() { }
        public MediaButt(Keys butt) { Butt = butt; }

        public Keys Butt { get; set; }

        public void Click()
        {
            keybd_event((byte)Butt, 0, 1, 0);
            keybd_event((byte)Butt, 0, 1 | 2, 0);
        }
    }
    class LockWorkStation
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockSt();
        public LockWorkStation() { }
        public void Lock()
        {
            LockSt();
        }
    }
}
