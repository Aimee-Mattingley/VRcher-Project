using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour 
{

	public float jumpSpeed = 600.0f;
	public bool grounded = false;
	public bool doubleJump = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private Animator anim;
	public Rigidbody rb;
	public float vSpeed;

	public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private int waypointChooser = 0;
    private System.Random r = new System.Random();
    private int rInt;
	public int health;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
		anim.SetBool("isIdle", true);
	}
	void Start ()
	{
		health = 2;
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
	void FixedUpdate () 
	{
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		vSpeed = rb.velocity.y;
        anim.SetFloat ("vSpeed", vSpeed);
	}
	void Update () 
	{
		Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            if(rInt == 0){GetNextWaypoint();}
            else if (rInt == 1){GetNextLeftWaypoint();}
            else if (rInt == 2){GetNextRightWaypoint();}
        }
		
		/*if (Input.GetKeyDown("space") && anim.GetBool("isIdle"))
		{
			Jump();
		}
		*/
	}

	public void Jump ()
	{
		if (grounded && rb.velocity.y == 0)
		{
			anim.SetTrigger("isJump");
            rb.AddForce(0,jumpSpeed,0, ForceMode.Impulse);
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}

	void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1){
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void GetNextLeftWaypoint()
    {
        if(wavepointIndex >= LeftWaypoints.points.Length - 1){
            return;
        }

        wavepointIndex++;
        target = LeftWaypoints.points[wavepointIndex];
    }
    void GetNextRightWaypoint()
    {
        if(wavepointIndex >= RightWaypoints.points.Length - 1){
            return;
        }

        wavepointIndex++;
        target = RightWaypoints.points[wavepointIndex];
    }

	public void enemyHit(){
		health -= 1;
		if(health <= 0){
			Destroy(gameObject);
		}
		
	}
}
