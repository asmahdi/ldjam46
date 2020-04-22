
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource themeMusic, oceanSound;

    public float fadeSpeed = 0.1f;

    
    
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
            themeMusic.volume = FadeValue(themeMusic.volume, 0f, fadeSpeed);
            oceanSound.volume = FadeValue(oceanSound.volume, DataManager.OCEAN_VOL, fadeSpeed);
        }
        else
        {
            themeMusic.volume = FadeValue(themeMusic.volume, DataManager.BGM, fadeSpeed);
            oceanSound.volume = FadeValue(oceanSound.volume, 0f, fadeSpeed);
        }

        //Debug.Log(oceanSound.volume);
    }


    private float FadeValue(float value, float result, float speed)
    {
        float t = 0;
        if (t < 1)
        {
            t += speed * Time.deltaTime;
        }
        return Mathf.Lerp(value, result, t);
        
    }


}
