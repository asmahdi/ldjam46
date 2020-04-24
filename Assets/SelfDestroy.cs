using UnityEngine;


public class SelfDestroy : MonoBehaviour
{
    public GameObject effect;

    private void Start()
    {
        DataManager.CLICKER++;
    }

    private void Update()
    {
        effect.transform.rotation =  Camera.main.transform.rotation;

        if (effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            DataManager.CLICKER--;
            Destroy(gameObject);
        }
        
    }
}
