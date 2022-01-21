using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthController : MonoBehaviour
{
    private float DefaultHealth = 100;
    [SerializeField] private GameObject Entity; 
    [SerializeField] private bool IsPlayer = false;
    [SerializeField] private GameObject HealthBar;
    private Slider healthSlider;
    [SerializeField] TextMeshProUGUI text;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; set; }

    private void Awake(){
        if(MaxHealth == 0){
            MaxHealth = DefaultHealth;
        }
        CurrentHealth = MaxHealth;
        if(IsPlayer && healthSlider == null){
            healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        }
        if(text == null){
            Debug.LogError("No HealthBar text component assigned!");
        }
        if(HealthBar == null){
            Debug.LogError("No HealthBar component assigned!");
        }else{
            healthSlider = HealthBar.GetComponent<Slider>();
        }
    }

    private void Start(){
        SetUpHealthBar();
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
    }

    public void DoDamage(float Damage){
        // Prevent Max Health One shot
        if(CurrentHealth == MaxHealth && CurrentHealth - Damage <= 0){
            Damage *= 0.95f;
        }else{
            CurrentHealth -= Damage;
        }
        AdjustHealthBar(-Damage);
        if(CurrentHealth <= 0.0f){
            //Death Logic

            Destroy(Entity);
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
}
