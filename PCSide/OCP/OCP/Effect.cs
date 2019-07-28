using System;
using System.Collections.Generic;
using System.Linq;
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
        public Brightness Brightness { get; set; }
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
    }

    class Volume
    {
        public Volume() { }
        public Volume(string audioDeviceID) { AudioDeviceID = audioDeviceID; }

        public string AudioDeviceID { get; set; }
    }
    class Brightness
    {
        public Brightness() { }
    }
    class Gamma
    {
        public Gamma() { }
        public Gamma(char color) { Color = color; }

        public char Color { get; set; }
    }
    class Reobas
    {
        public Reobas() { }
        public Reobas(string fanID) { FanID = fanID; }

        public string FanID { get; set; }
    }

    class Mute
    {
        public Mute() { }
        public Mute(string audioDeviceID) { AudioDeviceID = audioDeviceID; }

        public string AudioDeviceID { get; set; }
    }
    class RunFile
    {
        public RunFile() { }
        public RunFile(string file, string param) { File = file; Params = param; }

        public string File { get; set; }
        public string Params { get; set; }
    }
    class KeyboardShortcut
    {
        public KeyboardShortcut() { }
        public KeyboardShortcut(string shortcut) {Shortcut = shortcut; }

        public string Shortcut { get; set; }
    }
    class MediaButt
    {
        public MediaButt() { }
        public MediaButt(Keys butt) { Butt = butt; }

        public Keys Butt { get; set; }
    }
}
