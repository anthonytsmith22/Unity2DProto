using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private Vector3 fireDirection;
    private float waitTime = 2.5f;
    private void Awake(){
        
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

    private void OnCollisionEnter(Collision other){
        Destroy(this.gameObject);
    }

    private IEnumerator WaitAndDestroy(float waitTime){
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
