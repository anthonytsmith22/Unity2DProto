using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class AbilityUIController : MonoBehaviour
{
    [SerializeField] private Canvas AbilitySpriteCanvas;
    [SerializeField] private Image AbilitySpriteImage;
    [SerializeField] private Canvas UnusableCanvas;
    [SerializeField] private Image UnusableImage;
    [SerializeField] private Canvas CooldownTextCanvas;
    [SerializeField] private TextMeshProUGUI CooldownText;
    [SerializeField] private Canvas AbilitityChargesCanvas;
    [SerializeField] private TextMeshProUGUI AbilityChargesText;

    [SerializeField] private GameObject Player;
    [SerializeField] private Ability AbilityController;
    [SerializeField] private PlayerAbilities abilities;
    public Ability.AbilityType Type = Ability.AbilityType.None;
    private void Awake(){
        if(Player == null){
            Player = GameObject.Find("Player");
        }        
        abilities = Player.GetComponent<PlayerAbilities>();
        switch(Type){
            case Ability.AbilityType.Primary:
                AbilityController = abilities.PrimaryAbility;
                break;
            case Ability.AbilityType.Secondary:
                AbilityController = abilities.SecondaryAbility;
                break;
            case Ability.AbilityType.Mobility:
                AbilityController = abilities.MovementAbility;
                break;
            case Ability.AbilityType.Tech:
                AbilityController = abilities.TechAbility;
                break;
            case Ability.AbilityType.None:
                Debug.LogError("No ability type specified in AbillityUIController.");
                break;
        }
        AbilityController.OnChargeChangeEnter += UpdateChargeText;
        AbilityController.OnChangeCooldownTimeRemaingEnter += ToggleEnableUpdate;
    }
    private StringBuilder ChargesSB = new StringBuilder();
    private int Charges;
    public void UpdateChargeText(){
        ChargesSB.Clear();
        Charges = AbilityController.Charges;
        ChargesSB.Append(Charges);
        AbilityChargesText.text = ChargesSB.ToString();
    }
    
    private bool EnableUpdateCooldownText = false;
    public void ToggleEnableUpdate(){
        EnableUpdateCooldownText = !EnableUpdateCooldownText;
    }
    private StringBuilder CooldownTimeSB = new StringBuilder();
    private float CurrentCooldownTime;
    public void UpdateCooldownText(){
        if(EnableUpdateCooldownText && Charges == 0){
            UnusableImage.enabled = true;
            CurrentCooldownTime = AbilityController.CurrentCooldownTime;
            CooldownTimeSB.Clear();
            CooldownTimeSB.Append(CurrentCooldownTime.ToString("0.00"));
            CooldownText.text = CooldownTimeSB.ToString();
        }else{
            CooldownTimeSB.Clear();
            CooldownText.text = CooldownTimeSB.ToString();
            UnusableImage.enabled = false;
        }
    }

    private void Update(){
        UpdateCooldownText();
    }
}
