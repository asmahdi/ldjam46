using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoverC")
        {
            transform.SetParent(other.transform);
            other.GetComponent<Animator>().SetTrigger("goto_gate");
        }
    }


   
}
