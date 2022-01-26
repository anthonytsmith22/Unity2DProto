using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class QuantumEntanglementAbility : Ability
{
    public CircleCollider2D TriggerArea;
    public List<EnemyHealth> PotentialEnemies = new List<EnemyHealth>();
    public List<EnemyHealth> TargetedEnemies = new List<EnemyHealth>();
    public const float TriggerAreaRaduis = 7.5f;
    public const float Intensity = 100f;
    public float CurrentIntensity;
    public float Duration = 10f; // 10 Seconds
    private void Awake(){
        CooldownTime = 40f;
        TriggerArea = GetComponent<CircleCollider2D>();
        Type = AbilityType.Tech;
        activasionKey = KeyCode.Q;
        TriggerArea.isTrigger = true;
        TriggerArea.radius = TriggerAreaRaduis;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Run()
    {
        base.Run();
    }

    private void OnTriggerEnter2D(Collider2D other){
        GameObject interest = other.gameObject;
        if(interest.tag.Equals("Enemy")){
            EnemyHealth enemy = interest.GetComponent<EnemyHealth>();

            if(!PotentialEnemies.Contains(enemy)){
                PotentialEnemies.Add(enemy);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        GameObject interest = other.gameObject;
        if(interest.tag.Equals("Enemy")){
            EnemyHealth enemy = interest.GetComponent<EnemyHealth>();
            if(PotentialEnemies.Contains(enemy)){
                PotentialEnemies.Remove(enemy);
            }
        }
    }

    private Coroutine DischargeRoutine;
    private void EntangleEnemies(){
        TargetedEnemies.Clear();
        TargetedEnemies = PotentialEnemies;
        CurrentIntensity = Intensity;
        float DischareRate = Intensity/Duration;
        DischargeRoutine = StartCoroutine(Discharge(DischareRate, CurrentIntensity));
    }

    private void OnEndAbility(){
        int i;
        int size = TargetedEnemies.Count;
        for(i = 0; i < size; i++){
            EnemyHealth Enemy = TargetedEnemies[i];
            Enemy.isEntangled = false;
            TargetedEnemies.Remove(Enemy);
        }
    }

    private IEnumerator Discharge(float DischareRate, float CurrentIntensity){
        while(CurrentIntensity > 0){
            yield return new WaitForEndOfFrame();
            CurrentIntensity -= DischareRate * Time.deltaTime;
        }
        OnEndAbility();
    }

    public void DoDamage(float Damage, EnemyHealth Enemy){
        float actualDamage = CurrentIntensity * Damage;
        int i;
        int enemies = TargetedEnemies.Count;
        for(i = 0; i < enemies; i++){
            EnemyHealth currentEnemy = TargetedEnemies[i];
            if(currentEnemy != Enemy){
                currentEnemy.DoDamage(actualDamage);
            }
        }
    }

    public void RemoveTarget(EnemyHealth Enemy){
        if(TargetedEnemies.Contains(Enemy)){
            TargetedEnemies.Remove(Enemy);
            Enemy.isEntangled = false;
        }
    }
}
