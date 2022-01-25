using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private CircleCollider2D attackRange;
    [SerializeField] private float attackRadius = 4f;
    [SerializeField] List<Collider2D> Colliders = new List<Collider2D>();

    // If enemy is engaged with the player in combat
    private bool Engaged = false;
    // When player leaves enemy engagement radius, it will not immeditately disengage
    // Will wait EngagedExitTime before disengaging if the player does not enter
    // engagement radius
    private float EngagedExitTime = 3f;
    private Coroutine CancelEngagement;
    private bool CancelEngagementRunning = false;

    private void Start(){
        if(Target == null){
            Target = GameObject.Find("Player").transform.GetChild(0).transform;
        }
        attackRange.radius = attackRadius;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            // If enemy is disengaging, cancel
            if(CancelEngagementRunning){
                StopCoroutine(CancelEngagement);
                CancelEngagementRunning = false;
            }
            EnterCombat();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            CancelEngagement = StartCoroutine(Disengage(EngagedExitTime));
        }
    }

    IEnumerator Disengage(float waitTime){
        CancelEngagementRunning = true;
        yield return new WaitForSeconds(waitTime);
        Engaged = false;
        CancelInvoke("Combat");
        CancelEngagementRunning = false;
    }

    private void EnterCombat(){
        if(Engaged){
            return;
        }
        InvokeRepeating("Combat", 0.5f, 0.8f);
        Engaged = true;
    }
    private void Combat(){
        Vector2 fireDirection = (Target.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // rb.AddForce(FirePoint.right * bulletForce, ForceMode2D.Impulse);
        BulletController controller = bullet.GetComponent<BulletController>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(attackRange, bulletCollider);
        if(Colliders.Count > 0){
            foreach(Collider2D collider in Colliders){
                Physics2D.IgnoreCollision(collider, bulletCollider);
            }
        }
        controller.SetUp(fireDirection, bulletSpeed, false);
    }

}
