
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    
    void Start()
    {
        DataManager.RESTART = false;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        if (DataManager.RESTART)
        {
            Destroy(gameObject);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Initiate.Fade("Start", Color.black, 1);
        }

        if (SceneManager.GetActiveScene().name == "Music")
        {
            GetComponent<AudioSource>().volume = 0.2f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
        }
    }


}
