using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : MonoBehaviour
{
    [Header("Sound Mixing")]
    [Range(0, 1)]
    public float backgroundMusic;
    [Range(0, 1)]
    public float footstep, tileMoving, fx, ocean, heartVolume;

    private void Update()
    {
        DataManager.BGM = backgroundMusic;
        DataManager.FOOTSTEP = footstep;
        DataManager.TILEMOVING = tileMoving;
        DataManager.FX = fx;
        DataManager.OCEAN_VOL = ocean;
        DataManager.HEART_VOL = heartVolume;
    }

}
