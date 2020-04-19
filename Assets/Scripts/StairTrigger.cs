using UnityEngine;


public class StairTrigger : MonoBehaviour
{

    public Animator stair;
    public StairManager stairManager;
    public bool invert;

    private void OnTriggerEnter(Collider other)
    {
        if (invert)
        {
            stairManager.invert = true;
        }
        else
        {
            stairManager.invert = false;
        }

       if ( other.tag == "Player")
        {
            stair.SetBool("stair_up",true);
        }
    }


}
