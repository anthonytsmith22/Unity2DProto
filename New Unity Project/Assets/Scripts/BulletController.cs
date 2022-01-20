using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float spped = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Crosshair;
    private void Awake(){
        if(rb == null){
            rb = GetComponent<Rigidbody2D>();
        }
        if(Crosshair == null){
            Crosshair = GameObject.Find("Player").transform.GetChild(0).transform.Find("Crosshair").transform.GetChild(0).gameObject;
        } 
    }

    private void Start(){
        //rb.velocity();
    }
}
