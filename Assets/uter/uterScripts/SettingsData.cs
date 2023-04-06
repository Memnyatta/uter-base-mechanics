using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public int volume;
    public int music;
    public int sfx;
    public int language;
    public int graphics;
    public int resolution;
    public int display;
    public int vsync;
    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;
    public float brightness;

    public SettingsData(SettingsMenu data)
    {
        MasterVolume = data.MasterVolume;
        MusicVolume = data.MusicVolume;
        SFXVolume = data.SFXVolume;
        language = data.LanIndex;
        graphics = data.QuaIndex;
        resolution = data.ResIndex;
        display = data.DisIndex;
        vsync = data.VSIndex;
        brightness = data.BriIndex;
    }
}
