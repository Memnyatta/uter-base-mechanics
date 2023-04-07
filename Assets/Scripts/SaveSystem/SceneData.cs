using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int SceneNumberData;
    public string SceneNameData;
   
    public SceneData(DataChecker checker)
    {
        SceneNumberData = checker.SceneNumber;
        SceneNameData = checker.SceneName;
    }
}
