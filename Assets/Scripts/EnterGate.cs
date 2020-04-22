
using UnityEngine;

public class EnterGate : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("enter_gate");
            other.transform.SetParent(transform);
            Initiate.Fade(nextScene, Color.black, 1f);
            
        }
    }
}
