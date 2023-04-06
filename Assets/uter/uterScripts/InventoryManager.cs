using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image ItemImage1;
    public Image ItemImage2;
    public Image ItemImage3;
    public Image ItemImage4;

    public PauseMenu PauMen;
    public bool Paused;

    public bool InvFull = false;


    private void Update()
    {
        if (PauMen.GameIsPaused)
        {
            Paused = true;
        }
        else
        {
            Paused = false;
        }


        if (ItemImage1.sprite == null)
        {
            ItemImage1.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            ItemImage1.color = new Color32(255, 255, 255, 255);
        }

        if (ItemImage2.sprite == null)
        {
            ItemImage2.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            ItemImage2.color = new Color32(255, 255, 255, 255);
        }

        if (ItemImage3.sprite == null)
        {
            ItemImage3.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            ItemImage3.color = new Color32(255, 255, 255, 255);
        }

        if (ItemImage4.sprite == null)
        {
            ItemImage4.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            InvFull = true;
            ItemImage4.color = new Color32(255, 255, 255, 255);
        }
    }
}
