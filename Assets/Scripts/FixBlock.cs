
using UnityEngine;

public class FixBlock : MonoBehaviour
{
    public bool blockA, blockB, blockC;
    void Update()
    {
        if (DataManager.COMPLETE_BLOCK_A && blockA)
        {
            GetComponent<Animator>().SetTrigger("fix_block");
        }
        if(DataManager.COMPLETE_BLOCK_B && blockB)
        {
            GetComponent<Animator>().SetTrigger("fix_block");
        }
        if(DataManager.COMPLETE_BLOCK_C && blockC)
        {
            GetComponent<Animator>().SetTrigger("fix_block");
        }
    }
}
