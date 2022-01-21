using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    [SerializeField] private float Damage = 30f;
    private Collider2D bullet;
    private Vector3 fireDirection;
    private float waitTime = 2.5f;
    private void Awake(){
        bullet = GetComponent<Collider2D>();
    }

    private void Start(){
        StartCoroutine(WaitAndDestroy(waitTime));
    }

    private void Update(){
        transform.position += fireDirection * speed * Time.deltaTime;
    }

    public void SetUp(Vector3 FireDirection){
        this.fireDirection = FireDirection;
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag.Equals("Player")){
            Debug.Log("Check");
            Physics2D.IgnoreCollision(bullet, other.gameObject.GetComponent<Collider2D>());
        }else if(other.gameObject.tag.Equals("Enemy")){
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.DoDamage(Damage);
            Destroy(this.gameObject);
        }
        
    }

    private IEnumerator WaitAndDestroy(float waitTime){
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
