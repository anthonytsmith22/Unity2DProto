using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgressPlayer : MonoBehaviour
{
    [SerializeField] private CircleCollider2D AgressRange;
    [SerializeField] private float AgressRadius = 5f;
    [SerializeField] private AIPath Pathfinding;
    [SerializeField] private float CancelAgressWaitTime = 3f;
    private Coroutine CancelAgress;
    private bool CancelAgressIsRunning = false;
    private void Awake(){
        if(Pathfinding == null){
            Pathfinding = GetComponent<AIPath>();
        }
        Pathfinding.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            if(CancelAgressIsRunning){
                StopCoroutine(CancelAgress);
            }
            Pathfinding.enabled = true;
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
        Pathfinding.enabled = false;
        CancelAgressIsRunning = false;
    }
}
