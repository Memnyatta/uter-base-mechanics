using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;

    public bool[] kepkas = { false, false, false, false, false, false, false, false };

    public GameData (CurrentScene data)
    {
        level = data.scene;

        kepkas = data.kepkas;
    }

}
