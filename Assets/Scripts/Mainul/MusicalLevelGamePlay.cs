using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalLevelGamePlay : MonoBehaviour
{
    public GameObject level;
    public GameObject masterTile;
    public Material defaultMaterial;
    public Material redMaterial;
    public Material greenMaterial;
    public GameObject victoryCube;
    public GameObject rewardCube;
    public GameObject playerRewardCube;
    public GameObject endLevel;
    public float cubeSpeed;
    public GameObject lastCube;


    [Header("Audio Clips")]
    public AudioClip[] audioClips;
    public AudioClip wrongTileClip;

    private bool endMusic;


    bool isColoringActive;
    int coloredTileIndex;
    bool isLevelWon;
    bool isMovingToDestination;
    Vector3 destination;



    /*On Trigger Enter
     *
     *
     *
     *
     * End
     */

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "OceanTrigger")
        {
            if (!DataManager.OCEANSOUND)
            {
                DataManager.OCEANSOUND = true;
            }
            
        }


        if(other.gameObject == rewardCube)
        {
            
            Destroy(rewardCube);
            playerRewardCube.SetActive(true);
            gameObject.GetComponent<PlayerController>().isPlayerControllActive = false;
            destination = new Vector3(6,0,60);
            Invoke("EnableMovingToDestination", 2.0f);
        }

        if (other.gameObject == endLevel)
        {
            DataManager.OCEANSOUND = false;
            Initiate.Fade("GameB", Color.black, 1);

            endMusic = true;
        }

        if(other.gameObject == masterTile)
        {
            if (!isColoringActive)
            {
                isColoringActive = true;
                coloredTileIndex = 0;
                GuideToResetTile();
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Stop();
                audio.volume = DataManager.FX;
                audio.clip = audioClips[coloredTileIndex];
                audio.Play();
            }

            
        }

        if (isColoringActive)
        {
            if (other.gameObject.tag == "GreenTile" && other.gameObject != masterTile)
            {
                Renderer rend = other.gameObject.GetComponent<Renderer>();
                rend.enabled = true;
                if(rend.sharedMaterial != greenMaterial || other.gameObject == lastCube)
                {
                    rend.sharedMaterial = greenMaterial;
                    coloredTileIndex++;
                    //Debug.Log(coloredTileIndex);
                    AudioSource audio = gameObject.GetComponent<AudioSource>();
                    
                    if (coloredTileIndex == 16)
                    {
                        isLevelWon = true;
                        victoryCube.GetComponent<Animator>().SetBool("isLevelWon", true);
                        rewardCube.GetComponent<Animator>().SetBool("isLevelWon", true);
                        audio.loop = true;
                    } else
                    {
                        audio.loop = false;
                    }

                    if (coloredTileIndex < 17)
                    {
                        audio.Stop();
                        audio.volume = DataManager.FX;
                        audio.clip = audioClips[coloredTileIndex];
                        audio.Play();
                    }
                    
                    
                }
            }
            if (other.gameObject.tag == "RedTile" && !isLevelWon)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
               
                audio.clip = wrongTileClip;
                audio.Play();
                Renderer rend = other.gameObject.GetComponent<Renderer>();
                rend.enabled = true;
                rend.sharedMaterial = redMaterial;
                isColoringActive = false;
                coloredTileIndex = -1;
                Invoke("ResetAllTile", 0.75f);
            }
        }

        if ((other.gameObject.tag == "Untagged" || other.gameObject.tag == "OceanTrigger") && !isColoringActive)
        {
            coloredTileIndex = -1;
        }
        if ((other.gameObject.tag == "Untagged" || other.gameObject.tag == "OceanTrigger") && isColoringActive && !isLevelWon)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = wrongTileClip;
            audio.Play();
            Renderer rend = other.gameObject.GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = redMaterial;
            isColoringActive = false;
            coloredTileIndex = -1;
            Invoke("ResetAllTile", 0.75f);
            coloredTileIndex = -1;
            isMovingToDestination = false;
            Debug.Log(coloredTileIndex);
        }
    }



    /*On Trigger Stay
     *
     *
     *
     *
     * End
     */

    private void OnTriggerStay(Collider other)
    {
        if (isMovingToDestination)
        {
            if(other.gameObject == victoryCube)
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                victoryCube.GetComponent<Animator>().enabled = false;
                victoryCube.transform.position = Vector3.MoveTowards(victoryCube.transform.position, destination, cubeSpeed * Time.deltaTime);
                gameObject.transform.position = new Vector3(victoryCube.transform.position.x, 1, victoryCube.transform.position.z);
            }
        }

        if (endMusic)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();

            audio.volume = FadeValue(audio.volume, 0, 1f);
        }

        
    }





    //For reset the tiles

    void ResetAllTile()
    {
        int count = level.transform.childCount;

        for(int i = 0; i < count; i++)
        {
            Renderer rend = level.transform.GetChild(i).GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = defaultMaterial;
        }

        Invoke("GuideToResetTile", 0.5f);
    }



    void EnableMovingToDestination()
    {
        isMovingToDestination = true;
    }


    void GuideToResetTile()
    {
        Renderer rend = masterTile.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = greenMaterial;
        Renderer rend1 = lastCube.GetComponent<Renderer>();
        rend1.enabled = true;
        rend1.sharedMaterial = greenMaterial;
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
