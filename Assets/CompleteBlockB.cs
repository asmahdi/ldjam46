
using UnityEngine;

public class CompleteBlockB : MonoBehaviour
{
    public GameObject stair, lastStair, player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoverB")
        {
            DataManager.COMPLETE_BLOCK_B = true;
            print("Hit Player");
             Invoke("ResetStair", 5);
        }
    }


    private void ResetStair()
    {
        player.transform.SetParent(lastStair.transform);
        stair.GetComponent<Animator>().SetBool("stair_up", false);
    }
}
