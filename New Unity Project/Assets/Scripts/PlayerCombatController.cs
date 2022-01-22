using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float bulletForce = 20f;
 
    private Rigidbody2D rb;
    private void Awake(){
        if(FirePoint == null){
            FirePoint = GameObject.Find("Player").transform.GetChild(0).transform.Find("FirePoint").gameObject.GetComponent<Transform>();
        }
        if(bulletPrefab == null){
            Debug.LogError("No bullet prefab specified in PlayerCombatController.");
        }
        if(mainCamera == null){
            mainCamera = Camera.main;
        } 
        
    }
    private Vector3 mousePosition;
    // Direction FirePoint is rotated to
    private Vector3 lookDirection;
    private void Update(){
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        lookDirection = (mousePosition - new Vector3(FirePoint.position.x, FirePoint.position.y, FirePoint.position.z)).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        FirePoint.rotation = Quaternion.Euler(0f, 0f, angle);
        if(InputListener.Instance.primaryAttack){
            Shoot();
        }
    }

    private void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // rb.AddForce(FirePoint.right * bulletForce, ForceMode2D.Impulse);
        BulletController controller = bullet.GetComponent<BulletController>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), bulletCollider);
        controller.SetUp(lookDirection);

    }
}
