
using UnityEngine;

public class EnterGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("enter_gate");
            other.transform.SetParent(transform);
            Initiate.Fade("Music",Color.black, 1f);
            
        }
    }
}
