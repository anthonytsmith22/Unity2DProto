using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : Ability
{
    public Transform Player;
    public Transform Crosshair;
    public Camera Camera;

    private void Awake(){
        Type = AbilityType.Mobility;
        activasionKey = KeyCode.LeftShift;
        Charges = 2;
        if(Player == null){
            GameObject.Find("Player").GetComponent<Transform>();
        }
        if(Crosshair == null){
            Crosshair = Player.Find("Crosshair").GetComponent<Transform>();
        }
        if(Camera == null){
            Camera = Camera.main;
        }
    }

    public override void Start(){
        base.Start();
    }

    public override void Run()
    {
        Vector2 mousePosition = Input.mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(Camera.transform.position, mousePosition);
        if(hit.transform.tag.Equals("CollisionEnvironment")){
            return;
        }
        base.Run();
        Teleport(mousePosition);
    }

    private void Teleport(Vector2 TeleportPosition)
    {
        Player.position = TeleportPosition;
    }
}
