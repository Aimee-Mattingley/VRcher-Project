using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private bool isAttached = false;

    private bool isFired = false;

    void OnTriggerStay() {
        AttachArrow();
    }

    void OnTriggerEnter() {
        AttachArrow();
    }

    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired() {
        isFired = true;
    }

    private void AttachArrow() {
        var device = SteamVR_Controller.Input((int)ArrowManagerNew.Instance.trackedObj.index);
        if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
            ArrowManagerNew.Instance.AttachBowToArrow ();
            isAttached = true;
        }
    }

}
