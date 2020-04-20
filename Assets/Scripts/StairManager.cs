using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StairManager : MonoBehaviour
{
    [Range(0,6)]
    public float range;

    public bool invert;
    public GameObject[] stairObj;
    public float height;

    private int i;

   

    private void Update()
    {
        height = transform.position.y;

        if (invert)
        {
            for (i = stairObj.Length-1; i >= 0; i--)
            {
                stairObj[i].transform.position = new Vector3(stairObj[i].transform.position.x,
                                                                    height + range * (5-i) / 6,
                                                                  stairObj[i].transform.position.z);
            }
        }
        else
        {
            for (i = 0; i < stairObj.Length; i++)
            {
                stairObj[i].transform.position = new Vector3(stairObj[i].transform.position.x,
                                                                    height + range * i / 6,
                                                                  stairObj[i].transform.position.z);
            }
        }
       
        

    }
}
