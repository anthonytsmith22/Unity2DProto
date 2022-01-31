using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public Transform player;
    private void Awake(){
        player = GetComponent<Transform>();
    }

    public Vector2 movement;
    private void Update(){
        movement.x = InputListener.Instance.horizontal;
        movement.y = InputListener.Instance.vertical;
        Vector2 playerPosition = player.position;
        playerPosition += movement * movementSpeed * Time.deltaTime;
        player.position = playerPosition;
    }
}
