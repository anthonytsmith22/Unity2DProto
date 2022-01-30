using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    
    [SerializeField] private Transform Player;
    [SerializeField] private Transform CameraPoint;
    [SerializeField] private float radius = 10f;
    private Transform playerPosition;
    private Transform crosshairPosition;

    private void Awake(){
        if(Player == null){
            Player = GameObject.Find("Player").transform;
        }
        playerPosition = Player.GetChild(0).transform;
        crosshairPosition = GetComponent<Transform>();
    }

    private void Start(){
        crosshairPosition.position = playerPosition.position;
    }

    private Vector3 mousePosition = new Vector3(0f, 0f, 0f);
    [SerializeField] private float moveSpeed = .1f;
    private void Update(){
        // Get Camera bounds
        
        //Get Current position of play as it is our anchor for the camera
        Vector3 playerPosition = Player.position;
        Vector2 playerPosition2D = new Vector2(playerPosition.x, playerPosition.y);
        // Get current position of mouse
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Get offset mouse position is from our player and clamp our Camera point position
        Vector2 offset = mousePosition - playerPosition;
        CameraPoint.position = Vector2.Lerp(transform.position, playerPosition2D + Vector2.ClampMagnitude(offset, radius), moveSpeed);
        crosshairPosition.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

}
