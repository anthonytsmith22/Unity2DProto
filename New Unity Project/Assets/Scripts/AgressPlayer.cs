using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgressPlayer : MonoBehaviour
{
    [SerializeField] private CircleCollider2D AgressRange;
    private AIDestinationSetter destinationSetter;
    [SerializeField] private float AgressRadius = 5f;
    [SerializeField] private AIPath Pathfinding;
    [SerializeField] private float CancelAgressWaitTime = 3f;
    private Coroutine CancelAgress;
    private bool CancelAgressIsRunning = false;
    private void Awake(){
        if(Pathfinding == null){
            Pathfinding = GetComponent<AIPath>();
        }
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start(){
        AgressRange.radius = AgressRadius;
        destinationSetter.target = null;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            if(CancelAgressIsRunning){
                StopCoroutine(CancelAgress);
            }
            destinationSetter.target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            CancelAgress = StartCoroutine(CancelAggressRoutine(CancelAgressWaitTime));
        }
    }

    private IEnumerator CancelAggressRoutine(float waitTime){
        CancelAgressIsRunning = true;
        yield return new WaitForSeconds(CancelAgressWaitTime);
        destinationSetter.target = null;
        CancelAgressIsRunning = false;
    }
}
