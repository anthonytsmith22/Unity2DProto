using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public PlayerMovement movementController;
    public Rigidbody2D playerRB;
    
    private float DashSpeed = 2f;
    private void Awake(){
        Type = AbilityType.Mobility;
        activasionKey = KeyCode.LeftShift;
        playerRB = GetComponent<Rigidbody2D>();
        MaxCharges = 3;
        if(movementController == null){
            movementController = GetComponent<PlayerMovement>();
        }
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Run()
    {
        base.Run();
        Vector2 movement = new Vector2();
        movement.x = InputListener.Instance.horizontal;
        movement.y = InputListener.Instance.vertical;
        Debug.Log(movement);
        playerRB.position += movement.normalized * DashSpeed;
        Debug.Log("CheckRun2");
        DashRoutine = StartCoroutine(Dash(movement.normalized));
        //Dash2(movement);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(isDashing){
            StopCoroutine(DashRoutine);
            isDashing = false;
        }
    }

    private Coroutine DashRoutine;
    private bool isDashing = false;
    private IEnumerator Dash(Vector2 Direction){
        int i;
        isDashing = true;
        int frames = GameManager.Instance.FPS/6;
        for(i = 0; i < frames; i++){
            playerRB.position += Direction * DashSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
    }

    private void Dash2(Vector2 Direction){
        playerRB.AddForce(DashSpeed * Direction.normalized);
    }

}
