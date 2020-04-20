
using UnityEngine;

public class MoveToGateA : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
       
        if(DataManager.COMPLETE_BLOCK_A)
        {
            GetComponent<Animator>().SetTrigger("to_gate");
            if (other.tag == "Player")
            {
                other.transform.Rotate(0, -90, 0);
            }
        }
        
        
    }
}
