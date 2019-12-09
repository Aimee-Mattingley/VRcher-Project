using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private int waypointChooser = 0;
    
    Animator anim;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator>();
	}
    
    void Start()
    {
        target = Waypoints.points[0];
        anim.SetBool("isRun",!(anim.GetBool("isRun")));
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

}
