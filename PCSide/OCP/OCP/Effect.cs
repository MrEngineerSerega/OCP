using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    class Volume
    {
        public Volume() { Selected = true; }
        public Volume(string audioDeviceID) { Selected = true; AudioDeviceID = audioDeviceID; }

        public bool Selected { get; set; }
        public string AudioDeviceID { get; set; }
    }
    class Brightness
    {
        public Brightness() { Selected = true; }

        public bool Selected { get; set; }
    }
    class Gamma
    {
        public Gamma() { Selected = true; }
        public Gamma(char color) { Selected = true; Color = color; }

        public bool Selected { get; set; }
        public char Color { get; set; }
    }
    class Reobas
    {
        public Reobas() { Selected = true; }
        public Reobas(string fanID) { Selected = true; FanID = fanID; }

        public bool Selected { get; set; }
        public string FanID { get; set; }
    }

    class Mute
    {
        public Mute() { Selected = true; }
        public Mute(string audioDeviceID) { Selected = true; AudioDeviceID = audioDeviceID; }

        public bool Selected { get; set; }
        public string AudioDeviceID { get; set; }
    }
    class RunFile
    {
        public RunFile() { Selected = true; }
        public RunFile(string file) { Selected = true; File = file; }

        public bool Selected { get; set; }
        public string File { get; set; }
    }
    class KeyboardShortcut
    {
        public KeyboardShortcut() { Selected = true; }
        public KeyboardShortcut(string shortcut) { Selected = true; Shortcut = shortcut; }

        public bool Selected { get; set; }
        public string Shortcut { get; set; }
    }
}
