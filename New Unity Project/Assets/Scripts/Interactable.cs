using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject BorderHighlight;
    [SerializeField] private GameObject InteractBar;
    private SpriteRenderer HighLightSprite;
    private GameObject Player;
    private PlayerInteractionController InteractionController;
    public bool isInteractable { get; private set; }
    private bool isInteracting = false;

    [SerializeField] private Vector3 offset = new Vector3(1.2f, -.02f);

    private void Update(){
        if(isInteractable && !isInteracting){
            InteractBar.transform.position = Player.transform.position + offset;
        }
        if(isInteracting){
            InteractBar.SetActive(false);
        }
    }
    private void Awake(){
        isInteractable = false;
        HighLightSprite = BorderHighlight.GetComponent<SpriteRenderer>();
        HighLightSprite.enabled = false;
        InteractBar.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            Player = other.gameObject;
            InteractionController = Player.GetComponent<PlayerInteractionController>();
            InteractionController.AddInteractable(this);
        }
    }

    public void EnableInteraction(){
        isInteractable = true;
        HighLightSprite.enabled = true;
        InteractBar.SetActive(true);
        InteractBar.transform.position = Player.transform.position + offset;
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag.Equals("Player")){
            InteractionController.RemoveInteractable(this);
            Player = null;
            InteractionController = null;
            DisableInteraction();
        }
    }

    public void DisableInteraction(){
        isInteractable = false;
        HighLightSprite.enabled = false;
        InteractBar.SetActive(false);
    }
}
