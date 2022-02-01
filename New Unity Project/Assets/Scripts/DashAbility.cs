using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public PlayerMovement movementController;
    public Rigidbody2D playerRB;
    private float DashSpeed = 2f;
    public override void Awake(){
        base.Awake();
        Type = AbilityType.Mobility;
        activasionKey = KeyCode.LeftShift;
        playerRB = GetComponent<Rigidbody2D>();
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
        DashRoutine = StartCoroutine(Dash3(movement.normalized));
    }

    private void OnCollisionEnter2D(Collision2D other){
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other){
        isColliding = false;
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


    private float dashTime = 0.25f;
    private bool isColliding = false;
    private IEnumerator Dash3(Vector2 Direction){
        isDashing = true;
        float currentDashTime = dashTime;
        Vector2 currentPosition = Player.position;
        while(currentDashTime < 0.0f){
            if(isColliding){
                Player.position = currentPosition;
                isDashing = false;
                StopCoroutine(DashRoutine);
                yield return new WaitForEndOfFrame();
            }
            currentDashTime -= Time.deltaTime;
            currentPosition = Player.position;
            Vector2 newPosition = currentPosition + Direction * DashSpeed * Time.deltaTime;
            Player.position = newPosition;;
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
    }

}
