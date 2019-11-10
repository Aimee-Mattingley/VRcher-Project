using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    void OnTriggerStay() {
        AttachArrow();
    }

    void OnTriggerEnter() {
        AttachArrow();
    }

    private void AttachArrow() {
        var device = SteamVR_Controller.Input((int)ArrowManagerNew.Instance.trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
            ArrowManagerNew.Instance.AttachBowToArrow ();
        }
    }

}
