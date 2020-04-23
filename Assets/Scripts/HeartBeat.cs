using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartBeat : MonoBehaviour
{
    
    private void Update()
    {
        if (DataManager.COMPLETE_BLOCK_B)
        {
            GetComponent<Animator>().SetFloat("beat_speed", .67f);
        }
        else if (DataManager.COMPLETE_BLOCK_A)
        {
            GetComponent<Animator>().SetFloat("beat_speed", .4f);
        }
       
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            GetComponent<Animator>().SetFloat("beat_speed", 1f);
        }
    }

   


}
