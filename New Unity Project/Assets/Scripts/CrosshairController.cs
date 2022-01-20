using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    
    [SerializeField] GameObject Player;
    private Transform playerPosition;
    private Transform crosshairPosition;

    private void Awake(){
        if(Player == null){
            Player = GameObject.Find("Player");
        }
        playerPosition = Player.transform.GetChild(0).transform;
        crosshairPosition = GetComponent<Transform>();
    }

    private void Start(){
        crosshairPosition.position = playerPosition.position;
    }

    private Vector3 mousePosition = new Vector3(0f, 0f, 0f);
    [SerializeField] private float moveSpeed = 1f;
    private void FixedUpdate(){
        // mousePosition.x = Input.mousePosition.x;
        // mousePosition.y = Input.mousePosition.y;
        // crosshairPosition.position = mousePosition;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        crosshairPosition.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}
