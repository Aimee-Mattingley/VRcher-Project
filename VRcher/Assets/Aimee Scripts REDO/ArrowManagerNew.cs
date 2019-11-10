using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManagerNew : MonoBehaviour {

    public static ArrowManagerNew Instance;

    public SteamVR_TrackedObject trackedObj;

    private GameObject currentArrow;

    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPiont;

    public GameObject arrowPrefab;

    private bool isAttached = false;

    void Awake() {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy() {
        if (Instance == this)
            Instance = null;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    void Update() {
        AttachArrow();
        PullString();
    }

    private void PullString() {
        if (isAttached) {
            float dist = (stringStartPiont.transform.position - trackedObj.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPiont.transform.localPosition + new Vector3(0f, dist, 0f);
        }
}

private void AttachArrow() {
        if (currentArrow == null) {
            currentArrow = Instantiate (arrowPrefab);
            currentArrow.transform.parent = trackedObj.transform;
            currentArrow.transform.localPosition = new Vector3 (0f, -.03f, .01f);
        }
    }

    public void AttachBowToArrow() {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }
}