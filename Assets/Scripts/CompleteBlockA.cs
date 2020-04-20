
using UnityEngine;

public class CompleteBlockA : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MoverA")
        {
            DataManager.COMPLETE_BLOCK_A = true;
            print("Hit Player");
            Invoke("RotatePlayer", 1);
        }
    }


    private void RotatePlayer()
    {
        player.transform.Rotate(0, 180, 0);
    }
}
