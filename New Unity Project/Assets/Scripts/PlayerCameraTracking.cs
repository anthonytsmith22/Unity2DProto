using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTracking : MonoBehaviour
{
    private Transform playerCamera;
    private Transform playerPosition;

    private void Awake(){
        playerCamera = GetComponent<Transform>();
        playerPosition = GameObject.Find("Player").transform.GetChild(0).GetComponent<Transform>();
    }

    private void Update(){
        Vector3 position = playerPosition.position;
        position.z = -10;
        playerCamera.position = position;
    }
}
