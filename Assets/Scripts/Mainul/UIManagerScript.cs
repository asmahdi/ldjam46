using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMesh;

    public int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        textMesh.text = counter.ToString();
    }


    public void CounterButtonClicked()
    {
        counter++;
        textMesh.text = counter.ToString();
    }
}
