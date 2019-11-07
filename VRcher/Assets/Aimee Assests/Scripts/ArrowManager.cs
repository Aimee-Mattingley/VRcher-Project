using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager Instance;

    public SteamVR_TrackedObject trackedObj;

    private GameObject currentArrow;

    public GameObject stringattachPiont;
    public GameObject arrowStartPoint;

    public GameObject ArrowPrefab;

    void Awake() {
        if (Instance == null)
            Instance = this;
    }

    void OnDestory() {
        if (Instance == this)
            Instance = null;
    }

    // Use this for initialization 
    void Start() {
        
    }

    void Update() {
        AttachArrow();
    }

    private void AttachArrow() {
        if (currentArrow == null) {
            currentArrow = Instantiate (ArrowPrefab);
            currentArrow.transform.parent = trackedObj.transform;
            currentArrow.transform.localPosition = new Vector3 (0f, -.039f, .032f);

        }
    }

    public void AttachBowToArrow() {
        currentArrow.transform.parent = stringattachPiont.transform;
        currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

    }

}
