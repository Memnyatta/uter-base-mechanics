using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using NaughtyAttributes;

public class KepkaSOHandler : MonoBehaviour
{
    [SerializeField]
    KepkaSO kepkaSO;
    [SerializeField]
    Image kepkaImage;

    private void Start()
    {
        ApplyKepkaSO();
    }

    [Button]
    void ApplyKepkaSO()
    {
        if (kepkaSO != null)
        {
            kepkaImage.sprite = kepkaSO._image;
        }
    }
}
