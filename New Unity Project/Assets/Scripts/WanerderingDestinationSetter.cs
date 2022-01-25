using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WanerderingDestinationSetter : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private float WanderingSpeed = 0.5f;
    private float OriginalSpeed;
    private IAstarAI ai;    
    private AIPath pathing;
    private AIDestinationSetter destinationSetter;
    private Transform aiTransform;
    private void Start(){
        ai = GetComponent<IAstarAI>();
        aiTransform = GetComponent<Transform>();
        pathing = GetComponent<AIPath>();
        OriginalSpeed = pathing.maxSpeed;
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private Vector3 PickRandomPoint(){
        Vector3 point = Random.insideUnitSphere * radius;
        point.z = 0.0f;
        point += aiTransform.position;
        return point;
    }

    private void Update(){
        if(!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)){
            pathing.maxSpeed = WanderingSpeed;
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
        if(destinationSetter.target != null){
            pathing.maxSpeed = OriginalSpeed;
        }
    }
}
