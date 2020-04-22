using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TileMovingAudioScript : MonoBehaviour
{

    AudioSource audio;
    Vector3 previousPosition;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        if(currentPosition != previousPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        if (isMoving)
        {
            if (!audio.isPlaying)
            {

                audio.volume = 0.4f;
                audio.Play();
            }
            
        }
        else
        {
            audio.Stop();
        }
        Debug.Log(isMoving);

        previousPosition = currentPosition;
    }
}
