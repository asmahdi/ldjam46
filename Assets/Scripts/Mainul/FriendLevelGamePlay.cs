using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendLevelGamePlay : MonoBehaviour
{

    public float time;
    public GameObject friendA;
    public GameObject friendAFirstTrigger;
    public GameObject friendAFirstCube;
    public GameObject friendAThirdCube;
    public GameObject friendADestination;
    public GameObject bridge;


    public Material friendAFirstTriggerMaterial;
    public Material friendASecondTriggerMaterial;
    public Material defaultCubeMaterial;
    public float tileSpeed;

    public GameObject regularCube;


    [Header("Audio Clips")]
    public AudioClip activateClip;


    float tempTime;
    bool isFriendAFirstTriggerActivated;
    PlayerController pc;
    bool isDestroyedFirstTrigger;

    bool isActivatedAudioPlayed;

    void Start()
    {
        isDestroyedFirstTrigger = false;
        pc = gameObject.GetComponent<PlayerController>();
        tempTime = time;
        isActivatedAudioPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFriendAFirstTriggerActivated)
        {
            MoveToDestination(friendAFirstCube, new Vector3(2, 0, 3));
        } else
        {
            MoveToDestination(friendAFirstCube, new Vector3(-3, 0, 3));
        }

        if (pc.isFriendAReached)
        {
            AnimateFriendA();
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == friendAFirstTrigger)
        {
            tempTime -= Time.deltaTime;
            if(tempTime <= 0)
            {
                ActivateFriendAFirstTrigger();
                if (!isActivatedAudioPlayed)
                {
                    AudioSource audio = gameObject.GetComponent<AudioSource>();
                    audio.clip = activateClip;
                    audio.volume = DataManager.FX;
                    audio.Play();
                    isActivatedAudioPlayed = true;
                }
            }
        }
        if (other.gameObject == friendAThirdCube)
        {
            tempTime -= Time.deltaTime;
            if (tempTime <= 0)
            {
                ActivateFriendASecondTrigger();
                if (!isActivatedAudioPlayed)
                {
                    AudioSource audio = gameObject.GetComponent<AudioSource>();
                    audio.clip = activateClip;
                    audio.volume = DataManager.FX;
                    audio.Play();
                    isActivatedAudioPlayed = true;
                }
            }
        }


    }


   void MoveToDestination(GameObject obj, Vector3 destination)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, destination, tileSpeed * Time.deltaTime);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == friendAFirstTrigger)
        {
            Invoke("DeactivateFriendAFirstTrigger", 1.5f);
        }
    }


    //Activate and Deactivate Friend A First Trigger
    //Activate
    void ActivateFriendAFirstTrigger()
    {
        Renderer rend = friendAFirstTrigger.GetComponent<Renderer>();
        rend.sharedMaterial = friendAFirstTriggerMaterial;
        isFriendAFirstTriggerActivated = true;
    }
    //Deactivate
    void DeactivateFriendAFirstTrigger()
    {
        isActivatedAudioPlayed = false;
        tempTime = time;
        Renderer rend = friendAFirstTrigger.GetComponent<Renderer>();
        rend.sharedMaterial = defaultCubeMaterial;
        isFriendAFirstTriggerActivated = false;
    }


    //Activate and Deactivate Friend A Second Trigger
    //Activate
    void ActivateFriendASecondTrigger()
    {
        Renderer rend = friendAThirdCube.GetComponent<Renderer>();
        rend.sharedMaterial = friendASecondTriggerMaterial;
        pc.isfriendASecondTriggerActivated = true;
        friendAThirdCube.GetComponent<Animator>().SetBool("isSecondTriggerActivated", true);
        friendADestination.GetComponent<Animator>().SetBool("isSecondTriggerActivated", true);
        bridge.GetComponent<Animator>().SetBool("isSecondTriggerActivated", true);
        if (!isDestroyedFirstTrigger)
        {
            Destroy(friendAFirstTrigger);
            Instantiate(regularCube, new Vector3(2, 0, 2), Quaternion.identity);
            isDestroyedFirstTrigger = true;
        }
    }

    //Deactivate()
    void DeactivateFriendASecondTrigger()
    {
        tempTime = time;
    }



    //Take Friend A to Grid
    void AnimateFriendA()
    {
        friendA.GetComponent<Animator>().enabled = true;
        friendA.GetComponent<Animator>().SetBool("DestinationAReached", true);
    }
}
