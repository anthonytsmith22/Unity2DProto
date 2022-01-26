using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class HealthController : MonoBehaviour
{
    [SerializeField] private float DefaultHealth = 100;
    [SerializeField] private GameObject Entity; 
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject HealthSlider;
    private Slider healthSlider;
    [SerializeField] TextMeshProUGUI text;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; set; }
    [SerializeField] private Vector3 Offset = new Vector3(0f, 0f, 0f);

    [SerializeField] private float DefaultShield = 50f;
    public float MaxShield { get; private set; }
    public float CurrentShield { get; private set; }
    [SerializeField] private GameObject ShieldBar;
    [SerializeField] private GameObject ShieldSlider;
    private Slider shieldSlider;
    [SerializeField] private TextMeshProUGUI ShieldText;

    private void Awake(){
        healthSlider = HealthSlider.GetComponent<Slider>();
        MaxHealth = DefaultHealth;
        CurrentHealth = MaxHealth;
        if(text == null){
            Debug.LogError("No HealthBar text component assigned!");
        }
        shieldSlider = ShieldSlider.GetComponent<Slider>();
        MaxShield = DefaultShield;
        CurrentShield = MaxShield;
    }

    private void Start(){
        SetUpHealthBar();
        SetUpShieldBar();
        //OnCombatEnter += CancelShieldRegen;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            DoDamage(10f);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            DoHeal(10f);
        }
        if(Input.GetKeyDown(KeyCode.T)){
            UpdateMaxHealth(20f);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            UpdateMaxHealth(400);
        }
        RegenShieldController();
        RegenHealthController();
    }

    public void DoDamage(float Damage){
        // Prevent Max Health One shot
        if(CurrentShield > 0.0f){
            CurrentShield -= Damage;
            if(CurrentShield < 0.0f){
                float remainder = -CurrentShield;
                CurrentShield = 0.0f;
                if(CurrentHealth == MaxHealth && CurrentHealth - remainder <= 0){
                    remainder *= 0.9f;
                }else{
                    CurrentHealth -= remainder;
                }
                AdjustHealthBar(-remainder);
                AdjustShieldBar(0.0f);
                CancelRegen();
            }else{
                Debug.Log("Check");
                // Shield takes all damage
                AdjustShieldBar(-Damage);
                CancelRegen();
                return;
            }
        }else{
            if(CurrentHealth == MaxHealth && CurrentHealth - Damage <= 0){
                Damage *= 0.95f;
            }else{
                CurrentHealth -= Damage;
            }
            AdjustHealthBar(-Damage);
            CancelRegen();
        }
        if(CurrentHealth <= 0.0f){
            //Death Logic
            Destroy(Entity);
            GameManager.Instance.Restart();   
        }
    }

    public void DoHeal(float Heal){
        if(CurrentHealth + Heal >= MaxHealth){
            CurrentHealth = MaxHealth;
        }else{
            CurrentHealth += Heal;
        }
        AdjustHealthBar(Heal);
    }

    private void SetUpHealthBar(){
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = MaxHealth;
         
        AdjustHealthBarString(); 
    }
    private void AdjustHealthBar(float change){
        healthSlider.value += change;
        AdjustHealthBarString();
    }

    private void AdjustHealthBarString(){
        string healthBarString = CurrentHealth + "/" + MaxHealth;
        text.text = healthBarString;
    }

    private void UpdateMaxHealth(float AddMaxHealth){
        float previousMax = MaxHealth;
        MaxHealth += AddMaxHealth;
        healthSlider.maxValue = MaxHealth;

        float currentHealth = CurrentHealth;
        float percentCurrentHealth = currentHealth/previousMax;

        float roundingCheck = (percentCurrentHealth * 10) % 10;
        if(roundingCheck >= 5){
            CurrentHealth = Mathf.Ceil(MaxHealth * percentCurrentHealth);
        }else{
            CurrentHealth = Mathf.Floor(MaxHealth * percentCurrentHealth);
        }
        healthSlider.value = CurrentHealth;
        
        AdjustHealthBarString();
        
    }

    private void UpdateMaxShield(float AddMaxShield){
        float previousMax = MaxShield;
        MaxShield += AddMaxShield;
        shieldSlider.maxValue = MaxShield;

        float currentShield = CurrentShield;
        float percentCurrentShield = currentShield/previousMax;

        float roundingCheck = (percentCurrentShield * 10) % 10;
        if(roundingCheck >= 5){
            CurrentShield = Mathf.Ceil(MaxShield * percentCurrentShield);
        }else{
            CurrentShield = Mathf.Floor(MaxShield * percentCurrentShield);
        }
        shieldSlider.value = CurrentShield;

        AdjustShieldBarString();
    }

    private void AdjustShieldBarString(){
        string shieldBarString = CurrentShield + "/" + MaxShield;
        ShieldText.text = shieldBarString;
    }

    private void SetUpShieldBar(){
        shieldSlider.maxValue = MaxShield;
        shieldSlider.value = MaxShield;
         
        AdjustShieldBarString(); 
    }

    private void AdjustShieldBar(float change){
        if(change == 0.0f){
            shieldSlider.value = 0;
        }else{
            shieldSlider.value += change;
            if(shieldSlider.value >= MaxShield){
                shieldSlider.value = MaxShield;
            }
        }
        AdjustShieldBarString();
    }
    

    [SerializeField] private float ShieldRegenRate = 0.33f; //Time between increments
    [SerializeField] private float ShieldRegenMagnitude = 1f; //Amount of shield added in increment
    [SerializeField] private float ShieldRegenDelay = 3f; // Amount of time after taking damage that regen starts
    private bool isRegening = false;
    private void RegenShieldController(){
        // Check if CurrentShield is at MaxValue, if so, check if we are regening, cancel invoke and return
        // if first check fails, we are below maxShield, need to regen, begin InvokeRepeating
        if(CurrentShield >= MaxShield){
            if(!isRegening){
                return;
            }
            CancelInvoke("RegenShield");
            isRegening = false;
        }
        if(!isRegening){
            InvokeRepeating("RegenShield", ShieldRegenDelay, ShieldRegenRate);
            isRegening = true;
        }
    }

    private void RegenShield(){
        CurrentShield += ShieldRegenMagnitude;
        AdjustShieldBar(ShieldRegenMagnitude);
    }

    private void CancelRegen(){
        if(isRegening){
            CancelInvoke("RegenShield");
            isRegening = false;
        }
        if(isRegeningHealth){
            CancelInvoke("RegenHealth");
            isRegeningHealth = false;
        }
    }

    [SerializeField] private float HealthRegenRate = 0.33f; //Time between increments
    [SerializeField] private float HealthRegenMagnitude = 1f; //Amount of shield added in increment
    [SerializeField] private float HealthRegenDelay = 3f; // Amount of time after taking damage that regen starts
    private bool isRegeningHealth = false;
    private void RegenHealthController(){
        if(CurrentHealth >= MaxHealth){
            if(!isRegeningHealth){
                return;
            }
            CancelInvoke("RegenHealth");
            isRegeningHealth = false;
        }
        if(!isRegeningHealth){
            InvokeRepeating("RegenHealth", HealthRegenDelay, HealthRegenRate);
            isRegeningHealth = true;
        }
    }

    private void RegenHealth(){
        CurrentHealth += HealthRegenMagnitude;
        AdjustHealthBar(ShieldRegenMagnitude);
    }
}
