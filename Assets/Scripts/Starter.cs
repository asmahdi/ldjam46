using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public void Start()
    {
        DataManager.COMPLETE_BLOCK_A = false;
        DataManager.COMPLETE_BLOCK_B = false;
        DataManager.COMPLETE_BLOCK_C = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Initiate.Fade("Art", Color.black, 1);
    }
}
