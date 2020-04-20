using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBlockC : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animator>().SetTrigger("fix_block");
    }

    
}
