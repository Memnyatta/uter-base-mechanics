using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    public SettingsMenu set;
    public CurrentScene scene;


    void LoadSettings()
    {
        set.ApplySettings();
    }

    void Start()
    {
        set.LoadPlayer();
        set.LoadSettings();
        scene.LoadPlayer();
        Invoke("LoadSettings", 0f);
    }
}
