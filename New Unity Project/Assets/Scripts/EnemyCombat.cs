using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform Target;
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public CircleCollider2D attackRange;
    public float attackRadius = 4f;
    public List<Collider2D> Colliders = new List<Collider2D>();

    // If enemy is engaged with the player in combat
    public bool Engaged = false;
    // When player leaves enemy engagement radius, it will not immeditately disengage
    // Will wait EngagedExitTime before disengaging if the player does not enter
    // engagement radius
    public float EngagedExitTime = 3f;
    public float EngagementEnterTime = 1.5f;
    public float AttackRate = 1.5f;
    public Coroutine CancelEngagement;
    public bool CancelEngagementRunning = false;

    public virtual void Awake(){

    }
    public virtual void Start(){
        if(Target == null){
            Target = GameObject.Find("Player").transform;
        }
        attackRange.radius = attackRadius;
    }

    public virtual void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            // If enemy is disengaging, cancel
            if(CancelEngagementRunning){
                StopCoroutine(CancelEngagement);
                CancelEngagementRunning = false;
                Engaged = true;
            }
            if(!Engaged){
                Engaged = true;
                EnterCombat();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            CancelEngagement = StartCoroutine(Disengage(EngagedExitTime));
        }
    }

    public virtual IEnumerator Disengage(float waitTime){
        CancelEngagementRunning = true;
        yield return new WaitForSeconds(waitTime);
        Engaged = false;
        CancelInvoke();
        CancelEngagementRunning = false;
    }

    public virtual void EnterCombat(){
        Engaged = true;
        InvokeRepeating("Combat", EngagementEnterTime, AttackRate);
    }
    public virtual void Combat(){
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
