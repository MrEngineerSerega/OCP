using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace OCP
{
    class Effects
    {
        public void SetVolumeDevice(int deviceID, int value)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
            devices[2].AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.00f;
        }
    }
}
