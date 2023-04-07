//Этот скрипт накладывается на объект в сцене и коллекционирует необходимую информацию.
//Чтобы модифицировать список информации, которую нужно собирать нужно модифицировать данный скрипт.

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataChecker : MonoBehaviour
{
    public int SceneNumber;
    public string SceneName;

    public bool saveOnQuit;

    [Button(enabledMode: EButtonEnableMode.Always)]
    public void DeleteSave()
    {
        SaveSystem.DeleteSave();
    }

    [Button(enabledMode: EButtonEnableMode.Always)]
    public void Save()
    {
        SaveSystem.SaveScene(this);
    }

    private void OnApplicationQuit()
    {
        SceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (saveOnQuit)
        {
            SaveSystem.SaveScene(this);
        }
    }
}
