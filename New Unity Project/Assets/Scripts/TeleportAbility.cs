using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : Ability
{
    public Transform Crosshair;
    

    public override void Awake(){
        base.Awake();
        Type = AbilityType.Mobility;
        activasionKey = KeyCode.LeftShift;
        CooldownTime = 10f;
        MaxCharges = 2;
        if(Player == null){
            GameObject.Find("Player").GetComponent<Transform>();
        }
        if(Crosshair == null){
            Crosshair = Player.Find("Crosshair").transform.GetChild(0).gameObject.GetComponent<Transform>();
        }
    }

    public override void Start(){
        base.Start();
    }

    public override void Run()
    {
        Debug.Log("Check Tele");
        // Vector2 mousePosition = Input.mousePosition;
        // RaycastHit2D hit = Physics2D.Raycast(Camera.transform.position, mousePosition);
        // if(hit.transform.tag.Equals("CollisionEnvironment")){
        //     //return;
        // }
        Vector3 destination = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        destination.z = 0.0f;
        base.Run();
        Teleport(destination);
    }

    private void Teleport(Vector3 TeleportPosition)
    {
        Player.position += Crosshair.localPosition;
    }
}
