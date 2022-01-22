using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorController : MonoBehaviour
{
    [SerializeField] private GameObject BorderHighlight;
    private SpriteRenderer HighLightSprite;
    private GameObject Player;
    public bool isInteractable { get; private set; }
    private bool isInteracting = false;

    private void Awake(){
        isInteractable = false;
        HighLightSprite = BorderHighlight.GetComponent<SpriteRenderer>();
        HighLightSprite.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            Player = other.gameObject;
            isInteractable = true;
            HighLightSprite.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            Player = null;
            isInteractable = false;
            HighLightSprite.enabled = false;
        }
    }

}
