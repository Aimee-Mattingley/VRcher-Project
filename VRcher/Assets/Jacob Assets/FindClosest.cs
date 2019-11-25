using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{

    public GameObject[] waypoints;
    Animator anim;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    float accuracyWP = 5.0f;
    int currentWP = 0;

    // Use this for initialization
    int FindClosestWP()
    {
        if (waypoints.Length == 0) return -1;
        int closest = 0;
        float lastDist = Vector3.Distance(this.transform.position, waypoints[0].transform.position);
        for (int i = 1; i < waypoints.Length; i++)
        {
            float thisDist = Vector3.Distance(this.transform.position, waypoints[i].transform.position);
            if (lastDist > thisDist && i != currentWP)
            {
                closest = i;
            }
        }
        return closest;
    }

    void Update()
    {
        Vector3 direction = waypoints[currentWP].transform.position - transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * speed);
        if (direction.magnitude < accuracyWP)
        {
            currentWP = FindClosestWP();
        }
    }
}