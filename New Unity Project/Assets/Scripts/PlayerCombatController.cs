using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake(){
        if(FirePoint == null){
            FirePoint = GameObject.Find("Player").transform.GetChild(0).transform.Find("FirePoint").gameObject.GetComponent<Transform>();
        }
    }

    private void Update(){
        if(InputListener.Instance.primaryAttack){
            Shoot();
        }
    }

    private void Shoot(){
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
