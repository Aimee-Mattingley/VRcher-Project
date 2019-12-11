using UnityEngine;
using System.Collections;

public class ArrowNejc : MonoBehaviour
{

    private bool isAttached = false;

    private bool isFired = false;

    public Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
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

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag  == "Enemy")
        {
            col.gameObject.GetComponent<CharMovement>().enemyHit();
        }
        
        rb.isKinematic = true;
        StartCoroutine(WaitAndDestroy());
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

    IEnumerator WaitAndDestroy(){
        yield return new WaitForSeconds(1);
        Destroy (this.gameObject);
    }

}