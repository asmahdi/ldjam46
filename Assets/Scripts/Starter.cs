using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    
    public void EndGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Initiate.Fade("Art", Color.black, 1);
    }
}
