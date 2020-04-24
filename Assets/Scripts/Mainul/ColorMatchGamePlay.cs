using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorMatchGamePlay : MonoBehaviour
{
    public GameObject level;
    public GameObject rewardStandee;
    public GameObject reward;
    public GameObject bridgeCube;
    public GameObject playerReward;
    public GameObject endLevel;
    public float cubeSpeed;

    [Header("Materials")]
    public Material emmisiveMaterial;
    public Material redMaterial;
    public Material defaultMaterial;
    public Color emissiveActivated, emissiveDecativated;


    [Header("Audio Clips")]
    public AudioClip wrongTileClip;
    public AudioClip triggerActivateClip;

    Material activaMaterial;

    int coloredDone;
    bool isGameWon;
    bool isBridgeReached;
    Vector3 destintion = new Vector3(3, 0, 40);


    // Start is called before the first frame update
    void Start()
    {
        activaMaterial = defaultMaterial;
        coloredDone = 0;
        isGameWon = false;
        isBridgeReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject == endLevel)
        {
            Initiate.Fade("GameA", Color.black, 1);
        }

        if (other.gameObject.tag == "Untagged")
        {
            if (activaMaterial == redMaterial && !isGameWon)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.volume = DataManager.FX;
                audio.clip = wrongTileClip;
                audio.Play();
            }
            activaMaterial = emmisiveMaterial;
            if(!isGameWon)
                ResetRedCubes();
        }
        if (other.gameObject.tag == "RedTile")
        {
            Renderer rend = other.gameObject.GetComponent<Renderer>();
            if(rend.sharedMaterial != redMaterial)
            {
                rend.sharedMaterial = activaMaterial;
                if(activaMaterial == redMaterial)
                    coloredDone++;
                if (coloredDone == 17)
                {
                    isGameWon = true;
                    rewardStandee.GetComponent<Animator>().SetBool("isLevelWon", true);
                    reward.GetComponent<Animator>().SetBool("isLevelWon", true);
                }
                    
            }
        }
        if (other.gameObject.tag == "GreenTile")
        {
            if (isGameWon)
            {
                
            } else
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.volume = DataManager.FX;
                audio.clip = triggerActivateClip;
                audio.Play();
            }

            if (activaMaterial != redMaterial)
            {
                activaMaterial = redMaterial;
            }

            emmisiveMaterial.SetColor("_ColorHigh", emissiveActivated);
            Invoke("BackToDefaultEmmisive", 1.0f);


        }

        if (other.gameObject.tag == "Reward")
        {
            Destroy(reward);
            playerReward.SetActive(true);
            bridgeCube.GetComponent<Animator>().SetBool("isRewardTaken", true);
        }

        if (other.gameObject.tag == "BridgeCubeTriggered" && !isBridgeReached)
        {
            gameObject.GetComponent<PlayerController>().isPlayerControllActive = false;
            Invoke("MakeIsBridgeReachedTrue", 0.75f);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BridgeCubeTriggered")
        {
            if (isBridgeReached)
            {
                other.gameObject.GetComponent<Animator>().enabled = false;
                other.transform.position = Vector3.MoveTowards(other.transform.position, destintion, cubeSpeed * Time.deltaTime);
                //Debug.Log(Time.deltaTime);
                gameObject.transform.position = new Vector3(other.transform.position.x, 1, other.transform.position.z);

                gameObject.GetComponent<PlayerController>().enabled = false;
            }

        }
    }


    void MakeIsBridgeReachedTrue()
    {
        isBridgeReached = true;
    }

    void ResetRedCubes()
    {
        coloredDone = 0;
        int childCount = level.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            if(level.transform.GetChild(i).tag == "RedTile")
            {
                Renderer rend = level.transform.GetChild(i).gameObject.GetComponent<Renderer>();
                rend.sharedMaterial = emmisiveMaterial;
            }
        }
    }


    void BackToDefaultEmmisive()
    {
        emmisiveMaterial.SetColor("_ColorHigh", emissiveDecativated);
    }

}
