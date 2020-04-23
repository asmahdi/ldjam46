using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
   
    void Start()
    {
        Invoke("ReloadGame", 15);
    }

    void ReloadGame()
    {
        Initiate.Fade("Start", Color.black, .5f);
        DataManager.RESTART = true;
    }
    
}
