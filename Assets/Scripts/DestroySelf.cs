
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
  
    void Update()
    {
        if (DataManager.COMPLETE_BLOCK_B)
        {
            Destroy(gameObject);
        }
    }
}
