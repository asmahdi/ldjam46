
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource themeMusic, oceanSound;
    
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

        if (DataManager.OCEANSOUND)
        {
            themeMusic.volume = 0f;
            //oceanSound.volume = 1f;
        }
        else
        {
            themeMusic.volume = 1;
            oceanSound.volume = 0;
        }
    }


}
