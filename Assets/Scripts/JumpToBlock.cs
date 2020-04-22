
using UnityEngine;

public class JumpToBlock : MonoBehaviour
{

    private GameObject player;
    private Collider other;

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
            this.other = other;
            Invoke("MoveC",.5f);
            //Invoke("EndLevel", 3);
        }
    }

    private void MoveC()
    {
        player.transform.SetParent(other.transform);
        other.GetComponent<Animator>().SetTrigger("goto_gate");
    }


    private void EndLevel()
    {
        Initiate.Fade("Friend", Color.black, 1);
    }
}
