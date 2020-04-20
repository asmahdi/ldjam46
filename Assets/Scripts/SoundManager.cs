
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Initiate.Fade("Start", Color.black, 1);
        }

        if (SceneManager.GetActiveScene().name == "Art")
        {
            GetComponent<AudioSource>().volume = 0.2f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
        }
    }


}
