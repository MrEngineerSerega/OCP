using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace OCP
{
    class Effects
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

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
        public void SetBrightness(int brightness)
        {
            IntPtr DC = GetDC(GetDesktopWindow());
            RAMP _Rp = new RAMP();

            GetDeviceGammaRamp(DC, ref _Rp);
            for (int i = 0; i < 256; i++)
            {
                int value = i * (brightness + 128);

                if (value > 65535)
                    value = 65535;

                _Rp.Red[i] = _Rp.Green[i] = _Rp.Blue[i] = Convert.ToUInt16(value);
            }

            SetDeviceGammaRamp(DC, ref _Rp);
        }
    }
}
