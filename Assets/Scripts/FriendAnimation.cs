
using UnityEngine;

public class FriendAnimation : MonoBehaviour
{
    public GameObject friendPlayer;

    private Vector3 previousPosition;
    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (previousPosition != transform.position)
        {
            friendPlayer.GetComponent<Animator>().SetBool("run", true);
        }
        else
        {
            friendPlayer.GetComponent<Animator>().SetBool("run", false);
        }

        previousPosition = transform.position;
    }
}
