using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Initiate.Fade("Start", Color.black, 1);
        }
    }


}
