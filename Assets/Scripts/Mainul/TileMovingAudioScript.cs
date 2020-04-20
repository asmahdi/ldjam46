using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TileMovingAudioScript : MonoBehaviour
{

    AudioSource audio;
    Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if(currentPosition != previousPosition)
        {
            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            if (audio.isPlaying)
                audio.Stop();
        }

        previousPosition = currentPosition;
    }
}
