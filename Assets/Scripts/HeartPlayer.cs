
using UnityEngine;

public class HeartPlayer : MonoBehaviour
{

    public Animator playerAnimator;
    public GameObject mover;
    public AudioSource footStep;

    private bool enableInput = false;

    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name);

               if(hit.transform.tag == "Heart" && enableInput)
                {
                    if (mover != null)
                    {
                        mover.GetComponent<Animator>().SetTrigger("put_block_b");
                    }
                }

                if (hit.transform.tag == "MoverC" )
                {
                    hit.transform.SetParent(transform);
                    transform.position = new Vector3(0, .5f, 0);
                }

            }
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {

        // FOR LEVEL 1
        if (other.tag == "MoverA")
        {
            print("Found");
            transform.parent.gameObject.GetComponent<Animator>().SetTrigger("kill");
            transform.SetParent(other.transform);
            other.GetComponent<Animator>().SetTrigger("put_block_a");
            
        }

        if ( other.tag == "GateBlock" && DataManager.COMPLETE_BLOCK_A)
        {
            transform.SetParent(null);
            transform.SetParent(other.transform);
        }


        

        if (other.tag == "MoverB")
        {
            transform.SetParent(null);
            transform.SetParent(other.transform);
            enableInput = true; 
        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RunPlayer")
        {
            playerAnimator.SetBool("run", true);
            footStep.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RunPlayer")
        {
            playerAnimator.SetBool("run", false);
            footStep.Stop();
        }
    }
}
