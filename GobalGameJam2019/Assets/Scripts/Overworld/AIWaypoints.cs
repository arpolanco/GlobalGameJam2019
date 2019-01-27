using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//reference: https://www.youtube.com/watch?v=GIDz0DjhA4E

public class AIWaypoints : MonoBehaviour
{
    List<Transform> waypoints = new List<Transform>();
    
    private Transform targetWaypoint;
    public Transform origWay;

    private Animator animator;

    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f; //If the distance between the enemy and the waypoint is less than this, then it has reacehd the waypoint
    private int lastWaypointIndex;

    public float movementSpeed = 1f;
    public float rotationSpeed = 1f;

    public float timePauseBeforeWalking = 1f;
    float timePause;
    bool isWaiting = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        foreach(Transform t in origWay)
        {
            waypoints.Add(t);
            t.GetComponent<MeshRenderer>().enabled = false;
            Destroy(t.GetComponent<BoxCollider>());
        }
        timePause = timePauseBeforeWalking;
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex]; //Set the first target waypoint at the start so the enemy starts moving towards a waypoint
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = rotationSpeed * Time.deltaTime;

        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        if (!isWaiting)
        {
            

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

            Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
            Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current target waypoint

            float distance = Vector3.Distance(transform.position, targetWaypoint.position);
            CheckDistanceToWaypoint(distance);

            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }
        else
        {
            timePause -= Time.deltaTime;
            rotationToTarget = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);
            if (timePause < 0)
            {
                timePause = timePauseBeforeWalking;
                isWaiting = false;
                animator.Play("Walk");
            }
                
            
        }
    }

    /// <summary>
    /// Checks to see if the enemy is within distance of the waypoint. If it is, it called the UpdateTargetWaypoint function 
    /// </summary>
    /// <param name="currentDistance">The enemys current distance from the waypoint</param>
    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    /// <summary>
    /// Increaes the index of the target waypoint. If the enemy has reached the last waypoint in the waypoints list, it resets the targetWaypointIndex to the first waypoint in the list (causes the enemy to loop)
    /// </summary>
    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
            waypoints.Reverse();
        }

        targetWaypoint = waypoints[targetWaypointIndex];
        isWaiting = true;
        animator.Play("Idle");
    }
}
