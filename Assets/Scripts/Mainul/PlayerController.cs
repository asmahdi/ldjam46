using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("For All Level")]
    public GameLevel gameLevel;
    public float gridMovementSpeed;
    public float gridRotationSpeed;
    public Camera cam;
    public GridBehaviour gridBehaviour;
    public bool isPlayerControllActive;
    

    [Header("For Friend Level")]
    public GameObject friendA;
    public GameObject friendAFirstTriggeredCube;
    public GameObject friendASecondCube;
    public GameObject friendAThirdCube;
    public GameObject friendADestinationCube;
    public GameObject victoryTile;
    public GameObject victoryCube;
    public bool isfriendASecondTriggerActivated;
    public bool isFriendAReached;
    public float interactionTime;


    [Header("For Player Animation")]
    public GameObject playerModel;



    CurrentMission currentMission;
    bool isPlayerMovingViaAnotherObject;
    bool isGridModeActive;
    Vector3 destination;
    float tempTimer;
    bool victoryCall;
    private Stack<GameObject> path;

    private int maxDepth = 20;
    private Vector3 previousPosition;

    AudioSource modelAudioSource;

    int frameCount;

    private void Start()
    {
        victoryCall = false;
        destination = transform.position;
        gridBehaviour.startX = (int) transform.position.x;
        gridBehaviour.startY = (int) transform.position.z;
        isGridModeActive = true;
        isPlayerMovingViaAnotherObject = false;
        currentMission = CurrentMission.FindTriggerA1;
        tempTimer = interactionTime;
        isPlayerControllActive = true;
        previousPosition = transform.position;
        modelAudioSource = playerModel.GetComponent<AudioSource>();
        frameCount = 1;
        path = new Stack<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        frameCount++;

        //Debug.Log("UPDATE " + frameCount);
        previousPosition = gameObject.transform.position;


        switch (gameLevel)
        {
            case GameLevel.Music:
                PlayerControllByClicking();
                break;
            case GameLevel.Friends:
                PlayerControllByClicking();
                TriggeredCubePlayerController();
                break;
            case GameLevel.Color:
                PlayerControllByClicking();
                break;
        }

        if (previousPosition == transform.position && isGridModeActive)
        {
            StopCharacterAnimationWithSound();
        }
    }

    private void LateUpdate()
    {
        
        //Debug.Log("LATE UPDATE " + frameCount);
    }




    void PlayerControllByClicking()
    {
        if (Input.GetMouseButtonDown(0) && !isPlayerMovingViaAnotherObject && !isfriendASecondTriggerActivated && isPlayerControllActive)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.position.x);
                //Debug.Log(hit.transform.position.z);

                if (isGridModeActive)
                {
                    if (friendAFirstTriggeredCube && hit.transform == friendAFirstTriggeredCube.transform && friendAFirstTriggeredCube.transform.position == new Vector3(2, 0, 3))
                    {
                        StartCharacterAnimationWithSound();
                        destination = new Vector3(2, 1, 3);
                        isGridModeActive = false;
                        return;
                    }
                    gridBehaviour.endX = (int)hit.transform.position.x;
                    gridBehaviour.endY = (int)hit.transform.position.z;
                    if(gridBehaviour.endX > -1 && gridBehaviour.endX < gridBehaviour.rows && gridBehaviour.endY > -1 && gridBehaviour.endY < gridBehaviour.columns)
                    {
                        gridBehaviour.findDistance = true;
                        Invoke("UpdatePath", 0.1f);
                    }
                }
                else
                {
                    if(currentMission == CurrentMission.FindTriggerA2)
                    {
                        if (hit.transform == friendAFirstTriggeredCube.transform || hit.transform == friendASecondCube.transform || hit.transform == friendAThirdCube.transform)
                        {
                            destination = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);
                            StartCharacterAnimationWithSound();
                        }
                    }
                } 
            }
        }

        if((path.Count > 0 || destination != transform.position) && isGridModeActive)
        {
            MoveToGridDestination();
        }
        if (!isGridModeActive)
        {
            MoveToNonGridDestination();
        }

        //setup start grid of player
        if(previousPosition.x > destination.x)
        {
            gridBehaviour.startX = (int)Mathf.Floor(transform.position.x);
        }
        else
        {
            gridBehaviour.startX = (int)Mathf.Ceil(transform.position.x);
        }
        if(previousPosition.z > destination.z)
        {
            gridBehaviour.startY = (int)Mathf.Floor(transform.position.z);
        }
        else
        {
            gridBehaviour.startY = (int)Mathf.Ceil(transform.position.z);
        }
        
        
    }







    void UpdatePath()
    {
        
        path = new Stack<GameObject>(gridBehaviour.path);

        if(path.Count > 0)
        {
            destination = path.Pop().transform.position + new Vector3(0,1,0);
        }
    }



    void MoveToGridDestination()
    {
        Vector3 targetDirection = destination - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

        Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);


        transform.position = Vector3.MoveTowards(transform.position, destination, gridMovementSpeed * Time.deltaTime);

        if (transform.position == destination && path.Count > 0)
        {
            destination = path.Pop().transform.position + new Vector3(0, 1, 0);
        }

        if (previousPosition != transform.position)
        {
            StartCharacterAnimationWithSound();
        }

    }




    void MoveToNonGridDestination()
    {

        Vector3 targetDirection = destination - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

        Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, destination, gridMovementSpeed * Time.deltaTime);

    }


    void TriggeredCubePlayerController()
    {
        //move player with green tile;
        if (!isGridModeActive)
        {
            if (friendAFirstTriggeredCube.transform.position != new Vector3(-3, 0, 3) && friendAFirstTriggeredCube.transform.position != new Vector3(2, 0, 3))
            {
                gameObject.transform.position = new Vector3(friendAFirstTriggeredCube.transform.position.x, 1, friendAFirstTriggeredCube.gameObject.transform.position.z);
                destination = new Vector3(-3, 1, 3);
                currentMission = CurrentMission.FindTriggerA2;
            }
            if(transform.position == new Vector3(2, 1, 3) || transform.position == new Vector3(-3, 1, 5) || transform.position == new Vector3(-3, 1, 4) || transform.position == new Vector3(-3, 1, 3))
            {
                StopCharacterAnimationWithSound();
            }

            if(transform.position == new Vector3(-3, 1, 5))
            {
                currentMission = CurrentMission.CompleteA2;
            }
        }



        if (isfriendASecondTriggerActivated)
        {
            if (!isFriendAReached)
            {
                StopCharacterAnimationWithSound();
            }
            else
            {
                if(transform.position == new Vector3(2,1,1))
                {
                    StopCharacterAnimationWithSound();
                }
            }
            
            //move in the second trigger
            if (gameObject.transform.position.z == 5 && !isFriendAReached)
            {
                Vector3 tempPos = new Vector3(friendAThirdCube.transform.position.x, 1, friendAThirdCube.transform.position.z);

                Vector3 targetDirection = new Vector3(2,1,5)  - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);

                gameObject.transform.position = tempPos;
                destination = tempPos;
            }

            //move to next red tile
            if(gameObject.transform.position.x == 2 && gameObject.transform.position.z < 6 && !isFriendAReached)
            {
                destination = new Vector3(2, 1, 6);
                StartCharacterAnimationWithSound();
            }

            //move with next tile with red tile
            if (gameObject.transform.position.x == 2 && gameObject.transform.position.z >= 6 && gameObject.transform.position.z < 8 && !isFriendAReached)
            {
                Vector3 tempPos = new Vector3(friendADestinationCube.transform.position.x, 1, friendADestinationCube.transform.position.z);

                Vector3 targetDirection = new Vector3(2, 1, 8) - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);

                gameObject.transform.position = tempPos;
                destination = tempPos;
            }


            if (gameObject.transform.position.x == 2 && gameObject.transform.position.z == 8 && !isFriendAReached)
            {
                tempTimer -= Time.deltaTime;

                if(tempTimer < 2 && tempTimer > 1)
                {
                    Vector3 td = transform.position - friendA.transform.position;
                    Vector3 nd = Vector3.RotateTowards(friendA.transform.forward, td, gridRotationSpeed * Time.deltaTime, 0.0f);

                    Debug.DrawRay(transform.position, nd, Color.green);

                    friendA.transform.rotation = Quaternion.LookRotation(nd);
                }
                if(tempTimer <= 0)
                {
                    isFriendAReached = true;
                    destination = new Vector3(2, 1, 1);
                    StartCharacterAnimationWithSound();
                }

                Vector3 targetDirection = friendA.transform.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);
            }



            if (gameObject.transform.position.x == 2 && gameObject.transform.position.z == 1 && isFriendAReached)
            {
                gameObject.GetComponent<Animator>().enabled = true;

                if (!victoryCall)
                {
                    victoryCall = true;
                    Invoke("FriendLevelVictory", 2.0f);
                }
            }

        }

    }


    void FriendLevelVictory()
    {
        victoryTile.GetComponent<Animator>().SetBool("isLevelWon", true);
        victoryCube.GetComponent<Animator>().SetBool("isLevelWon", true);
        Invoke("FriendLevelEnd", 4.0f);
    }

    void FriendLevelEnd()
    {
        Initiate.Fade("Ending", Color.black, .5f);
    }


    void StartCharacterAnimationWithSound()
    {
        playerModel.GetComponent<Animator>().SetBool("run", true);

        if (!modelAudioSource.isPlaying)
        {
            modelAudioSource.volume = DataManager.FOOTSTEP;
            modelAudioSource.Play();
        }
    }
    void StopCharacterAnimationWithSound()
    {
        playerModel.GetComponent<Animator>().SetBool("run", false);
        if (modelAudioSource.isPlaying)
        {
            modelAudioSource.Stop();
        }
            
    }


    enum CurrentMission
    {
        FindTriggerA1,
        FindTriggerA2,
        CompleteA2,
        FindTriggerB
    }

}


public enum GameLevel
{
    Music,
    Friends,
    Color
}