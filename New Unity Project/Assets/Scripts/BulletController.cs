using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float Damage = 30f;
    private bool isPlayer;
    private Collider2D bullet;
    private Vector3 fireDirection;
    [SerializeField] private float waitTime = 2.5f;
    private void Awake(){
        bullet = GetComponent<Collider2D>();
    }

    private void Start(){
        StartCoroutine(WaitAndDestroy(waitTime));
    }

    private void Update(){
        transform.position += fireDirection * speed * Time.deltaTime;
    }

    public void SetUp(Vector3 FireDirection, float bulletSpeed, bool isPlayer){
        this.fireDirection = FireDirection;
        this.isPlayer = isPlayer;
        this.speed = bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other){
        // If entity that fired this bulletPrefab isPlayer, then check for collision with Enemy tags
        // If !isPlayer then enemy fired and check collisions for player tags
        if(isPlayer){
            if(other.gameObject.tag.Equals("Enemy")){
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.DoDamage(Damage);
                Destroy(this.gameObject);
            }
        }
        else if(!isPlayer){
            if(other.gameObject.tag.Equals("Player")){
                HealthController playerHealth = other.gameObject.GetComponent<HealthController>();
                playerHealth.DoDamage(Damage);
                Destroy(this.gameObject);
            }
        }
        else if(other.gameObject.tag.Equals("CollisionEnvironment") || other.gameObject.layer.Equals("Obstacle")){
            Debug.Log("Check");
            Destroy(this.gameObject);
        }        
    }

    private IEnumerator WaitAndDestroy(float waitTime){
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
