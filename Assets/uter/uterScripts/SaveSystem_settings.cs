using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem_settings
{
    public static void SavePlayer(SettingsMenu settings)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SettingsSave.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settings);

        Debug.Log("После сохранения "+data.language);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadLevel()
    {
        string path = Application.persistentDataPath + "/SettingsSave.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            Debug.Log("language:");
            Debug.Log(data.language);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("SettingsSave File corrupted.");
            return null;
        }
    }
}
