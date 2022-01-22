using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private List<Interactable> possibleInteractables = new List<Interactable>();
    private Transform Player;
    private Interactable ClosestInteractable;

    private void Awake(){
        Player = GetComponent<Transform>();
    }

    private void Update(){
        GetClosestInterable();
    }

    public void AddInteractable(Interactable newInteractable){
        possibleInteractables.Add(newInteractable);
    }

    public void RemoveInteractable(Interactable interactable){
        possibleInteractables.Remove(interactable);
    }

    private void GetClosestInterable(){
        if(possibleInteractables.Count > 0){
            float distance = new Vector3(1000f, 1000f, 1000f).magnitude;
            int i;
            Interactable closest = possibleInteractables[0];
            foreach(Interactable interactable in possibleInteractables){
                float currentDistance = (interactable.transform.position - Player.position).magnitude;
                if(currentDistance < distance){
                    distance = currentDistance;
                    closest = interactable;
                }
            }
            if(ClosestInteractable != null){
                ClosestInteractable.DisableInteraction();
            }
            ClosestInteractable = closest;
            ClosestInteractable.EnableInteraction();
        }else{
            ClosestInteractable = null;
        }
    }
}
