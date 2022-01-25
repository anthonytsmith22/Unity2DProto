using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerController : MonoBehaviour
{
    public float OrthoCameraSize = 3f;
    private PolygonCollider2D confiner;
    [SerializeField] private GameObject CinemachineCamera;
    private CinemachineConfiner cameraConfiner;

    private void Awake(){
        if(CinemachineCamera == null){
            CinemachineCamera = GameObject.Find("PlayerCamera");
        }
        cameraConfiner = CinemachineCamera.GetComponent<CinemachineConfiner>();
        confiner = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            cameraConfiner.m_BoundingShape2D = confiner;
            GameManager.Instance.SetCameraOrthographicSize(OrthoCameraSize);
        }
    }

}
