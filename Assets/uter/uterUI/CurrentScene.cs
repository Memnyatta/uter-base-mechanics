 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : MonoBehaviour
{
    public int scene;

    private int f;

    public bool[] kepkas = {false, false, false, false, false, false, false, false};

    private void Start()
    {
        LoadPlayer();

        if (SceneManager.GetActiveScene().name == "Location_2")
        {
            kepkas[7] = false;
        }
        else if (SceneManager.GetActiveScene().name != "Location_2")
        {
            kepkas[7] = true;
        }
    }

    void Update()
    {
        f = SceneManager.GetActiveScene().buildIndex;

        if (f != 0)
        {
            if (scene < SceneManager.GetActiveScene().buildIndex)
            {
                scene = SceneManager.GetActiveScene().buildIndex;
            }
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void OnApplicationQuit()
    {
        if (f != 0)
        {
            SaveSystem.SavePlayer(this);
        }
    }

    public void LoadPlayer()
    {
        GameData data = SaveSystem.LoadLevel();

        kepkas = data.kepkas;

        scene = data.level;
    }
}
