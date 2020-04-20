
using UnityEngine;

public class CompleteBlockB : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoverB")
        {
            DataManager.COMPLETE_BLOCK_B = true;
            print("Hit Player");
           // Invoke("RotatePlayer", 1);
        }
    }


    private void RotatePlayer()
    {
        player.transform.Rotate(0, 180, 0);
    }
}
