
using UnityEngine;

public class Relaxing : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animator>().SetBool("relaxing", true);
    }
    
}
