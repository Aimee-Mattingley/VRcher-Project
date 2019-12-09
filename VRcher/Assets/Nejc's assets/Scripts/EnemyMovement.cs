using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private int waypointChooser = 0;
    public System.Random r = new System.Random();
    public int rInt;
    Animator anim;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator>();
	}
    
    void Start()
    {
        rInt = r.Next(0,3);
        if(rInt == 0){
            target = Waypoints.points[0];
        }
        else if (rInt == 1){
            target = LeftWaypoints.points[0];
        }
        else if (rInt == 2){
            target = RightWaypoints.points[0];
        }
        anim.SetBool("isRun",!(anim.GetBool("isRun")));
    }

    void Update()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            if(rInt == 0){GetNextWaypoint();}
            else if (rInt == 1){GetNextLeftWaypoint();}
            else if (rInt == 2){GetNextRightWaypoint();}
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1){
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void GetNextLeftWaypoint()
    {
        if(wavepointIndex >= LeftWaypoints.points.Length - 1){
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = LeftWaypoints.points[wavepointIndex];
    }
    void GetNextRightWaypoint()
    {
        if(wavepointIndex >= RightWaypoints.points.Length - 1){
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = RightWaypoints.points[wavepointIndex];
    }

}
