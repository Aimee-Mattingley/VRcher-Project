using UnityEngine;
using System.Collections;

public class ArrowNejc : MonoBehaviour
{
    [SerializeField] private int damageValue = 1;
    private bool isAttached = false;

    private bool isFired = false;
    private bool flying = true;
    private Transform anchor;

    void OnTriggerStay()
    {
        AttachArrow();
    }
    
    void OnTriggerEnter()
    {
        AttachArrow();
    }
    
    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
        else if (this.anchor != null){
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<Health>().Damage(damageValue);

        }


        if (this.isFired && col.gameObject.tag != "GameController")
        {
            this.isFired = false;

            this.transform.position = col.contacts[0].point;
            
            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = col.transform;
            this.anchor = anchor.transform;

            //Destroy(GetComponent<Rigidbody>());


            //Destroy(col.gameObject);
            //Rigidbody r = GetComponent<Rigidbody>();
            //r.isKinematic = true;

        }

    }

    public void Fired()
    {
        isFired = true;
    }

    private void AttachArrow()
    {
        var device = SteamVR_Controller.Input((int)ArrowManagerNejc.Instance.trackedObj.index);
        if (!isAttached && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            ArrowManagerNejc.Instance.AttachBowToArrow();
            isAttached = true;
        }
    }

}