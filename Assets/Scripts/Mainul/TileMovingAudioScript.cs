
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TileMovingAudioScript : MonoBehaviour
{

    AudioSource audio;
    float targetVolume;
    Vector3 previousPosition, currentPosition;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        previousPosition = transform.position;
        audio.volume = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetVolume = DataManager.TILEMOVING;
         currentPosition = transform.position;

        if(currentPosition != previousPosition)
        {
            isMoving = true;
            
        }
        else
        {
            isMoving = false;
            
        }


        
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            audio.volume = FadeValue(audio.volume, targetVolume, 2f);
            if (!audio.isPlaying)
            {
                audio.Play();
            }

        }
        else
        {
            audio.volume = FadeValue(audio.volume, 0, 5f);
            if (audio.isPlaying && audio.volume == 0)
            {
                audio.Pause();
            }
        }
        Debug.Log(isMoving);

        previousPosition = currentPosition;
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
