
using UnityEngine;

public class JumpToBlock : MonoBehaviour
{

    private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.transform.gameObject;
            other.transform.SetParent(transform);
            GetComponent<Animator>().SetTrigger("goto_block");

            
        }
        if (other.tag == "MoverC")
        {
            player.transform.SetParent(other.transform);
            other.GetComponent<Animator>().SetTrigger("goto_gate");

            Invoke("EndLevel", 3);
        }
    }

    void EndLevel()
    {
        Initiate.Fade("Friend", Color.black, 1);
    }
}
