using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelfKillB : MonoBehaviour
{
    
    void Update()
    {
        if (DataManager.COMPLETE_BLOCK_B)
        {
            Destroy(gameObject);
        }
    }
}
