using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 movement;
    private void Update(){
        movement.x = InputListener.Instance.horizontal;
        movement.y = InputListener.Instance.vertical;
    }

    private void FixedUpdate(){
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
