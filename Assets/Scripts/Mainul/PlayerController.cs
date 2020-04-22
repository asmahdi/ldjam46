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




    CurrentMission currentMission;
    bool isPlayerMovingViaAnotherObject;
    bool isGridModeActive;
    int pathCount;
    Vector3 destination;
    float tempTimer;
    bool victoryCall;

    private int maxDepth = 20;

    private void Start()
    {
        victoryCall = false;
        destination = transform.position;
        gridBehaviour.startX = (int) transform.position.x;
        gridBehaviour.startY = (int) transform.position.z;
        pathCount = 0;
        isGridModeActive = true;
        isPlayerMovingViaAnotherObject = false;
        currentMission = CurrentMission.FindTriggerA1;
        tempTimer = interactionTime;
        isPlayerControllActive = true;
    }


    // Update is called once per frame
    void Update()
    {


        
        switch(gameLevel)
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
                        destination = new Vector3(2, 1, 3);
                        isGridModeActive = false;
                        return;
                    }
                    gridBehaviour.endX = (int)hit.transform.position.x;
                    gridBehaviour.endY = (int)hit.transform.position.z;
                    if(gridBehaviour.endX > -1 && gridBehaviour.endX < gridBehaviour.rows && gridBehaviour.endY > -1 && gridBehaviour.endY < gridBehaviour.columns)
                    {
                        gridBehaviour.findDistance = true;
                        Invoke("InvokePathCount", 0.1f);
                    }
                }
                else
                {
                    if(currentMission == CurrentMission.FindTriggerA2)
                    {
                        if (hit.transform == friendAFirstTriggeredCube.transform || hit.transform == friendASecondCube.transform || hit.transform == friendAThirdCube.transform)
                        {
                            destination = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);
                        }
                    }
                } 
            }
        }

        if(pathCount > 0 && isGridModeActive)
        {
            MoveToGridDestination();
        }
        if (!isGridModeActive)
        {
            MoveToNonGridDestination();
        }

    }



    void InvokePathCount()
    {
        pathCount = gridBehaviour.path.Count;
    }


    void MoveToGridDestination()
    {
        
        destination = new Vector3(gridBehaviour.path[pathCount - 1].transform.position.x, gameObject.transform.position.y, gridBehaviour.path[pathCount  - 1].transform.position.z);
        Vector3 targetDirection = destination - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

        Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);


        transform.position = Vector3.MoveTowards(transform.position, destination, gridMovementSpeed * Time.deltaTime);

        gridBehaviour.startX = (int)transform.position.x;
        gridBehaviour.startY = (int)transform.position.z;

        if (transform.position.x == gridBehaviour.path[pathCount - 1].transform.position.x && transform.position.z == gridBehaviour.path[pathCount - 1].transform.position.z)
        {
            pathCount--;
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
        if (!isGridModeActive)
        {
            if (friendAFirstTriggeredCube.transform.position != new Vector3(-3,0,3) && friendAFirstTriggeredCube.transform.position != new Vector3(2, 0, 3))
             {
            
                gameObject.transform.position = new Vector3(friendAFirstTriggeredCube.transform.position.x, 1, friendAFirstTriggeredCube.gameObject.transform.position.z);
                destination = new Vector3(-3, 1, 3);
                currentMission = CurrentMission.FindTriggerA2;
            }     
        }

        if (isfriendASecondTriggerActivated)
        {
            //move in the second trigger
            if(gameObject.transform.position.z == 5 && !isFriendAReached)
            {
                Vector3 tempPos = new Vector3(friendAThirdCube.transform.position.x, 1, friendAThirdCube.transform.position.z);

                Vector3 targetDirection = new Vector3(2,1,5)  - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);

                gameObject.transform.position = tempPos;
                destination = tempPos;
            }

            //move to next tile
            if(gameObject.transform.position.x == 2 && gameObject.transform.position.z < 6 && !isFriendAReached)
            {
                destination = new Vector3(2, 1, 6);
            }

            //move with next tile
            if(gameObject.transform.position.x == 2 && gameObject.transform.position.z >= 6 && gameObject.transform.position.z < 8 && !isFriendAReached)
            {
                Vector3 tempPos = new Vector3(friendADestinationCube.transform.position.x, 1, friendADestinationCube.transform.position.z);

                Vector3 targetDirection = new Vector3(2, 1, 8) - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, gridRotationSpeed * Time.deltaTime, 0.0f);

                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);

                gameObject.transform.position = tempPos;
                destination = tempPos;
            }

            if(gameObject.transform.position.x == 2 && gameObject.transform.position.z == 8 && !isFriendAReached)
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
        Initiate.Fade("Ending", Color.black, 1f);
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