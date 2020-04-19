
using UnityEngine;

public class FixedRotator : MonoBehaviour
{
    public Vector3 speed;
    void Update()
    {
        transform.Rotate(speed*Time.deltaTime);
    }
}
