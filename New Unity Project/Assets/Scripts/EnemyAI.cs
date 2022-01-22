using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 3f;
    private float nextWayPointDistance = 3f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    private AIDestinationSetter TargetSetter;
    private void Awake(){
        // seeker = GetComponent<Seeker>();
        // rb = GetComponent<Rigidbody2D>();
        // // Freeze rotation of Z-axis
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rb.gravityScale = 0f;
        TargetSetter = GetComponent<AIDestinationSetter>();
        if(TargetSetter.target == null){
            TargetSetter.target = GameObject.Find("Player").transform.GetChild(0).transform;
        }
    }

    private void Start(){
        //InvokeRepeating("GeneratePath", 0f, 0.5f);
    }
    // private void FixedUpdate(){
    //     // If no path, return
    //     if(path == null){
    //         return;
    //     }

    //     // If current way point is equal to path waypoint count
    //     // reached end of path, return
    //     if(currentWayPoint >= path.vectorPath.Count){
    //         reachedEndOfPath = true;
    //         return;
    //     }else{
    //         reachedEndOfPath = false;
    //     }
        
    //     // If have no reached end of path, create vector2 towards next way point
    //     // From the vector of next way point minus the current enemy's position
    //     Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
    //     // Vector2 force = direction * speed * Time.fixedDeltaTime;
    //     // rb.AddForce(force);
    //     rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

    //     float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

    //     if(distance < nextWayPointDistance){
    //         currentWayPoint++;
    //     }
    // }

    // private void GeneratePath(){
    //     if(seeker.IsDone()){
    //         seeker.StartPath(rb.position, target.position, OnPathComplete);
    //     }
    // }

    // private void OnPathComplete(Path p){
    //     if(!p.error){
    //         path = p;
    //         currentWayPoint = 0;
    //     }
    // }
}
