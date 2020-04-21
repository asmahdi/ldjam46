using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelfKillA : MonoBehaviour
{
    
    void Update()
    {
        if (DataManager.COMPLETE_BLOCK_A)
        {
            Destroy(gameObject);
        }
    }
}
