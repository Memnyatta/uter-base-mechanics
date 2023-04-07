using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScene(DataChecker checker)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.codeiskain";
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData data = new SceneData(checker);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SceneData LoadSceneData()
    {
        string path = Application.persistentDataPath + "/Save.codeiskain";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogWarning("Нету файла с сейвом");
            return null;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/Save.codeiskain";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
