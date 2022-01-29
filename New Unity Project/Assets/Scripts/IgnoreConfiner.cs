using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreConfiner : MonoBehaviour
{
    public PolygonCollider2D MainCollider;
    public CompositeCollider2D compositeColider;

    public Camera mainCamera;
    public Collider cameraCollider;

    private void Awake(){
        MainCollider = GetComponent<PolygonCollider2D>();
        compositeColider = GetComponent<CompositeCollider2D>();
        mainCamera = Camera.main;
        cameraCollider = mainCamera.GetComponent<Collider>();
    }

    private void Start(){
        
    }
}
