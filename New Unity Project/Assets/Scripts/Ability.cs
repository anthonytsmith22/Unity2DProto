using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ability : MonoBehaviour
{
    public Transform Player;
    public AbilityType Type = AbilityType.None;
    public int Charges;
    public int MaxCharges = -1;
    public float CooldownTime = 0f;
    public bool CooldownRunning = false;
    public KeyCode activasionKey;
    public Coroutine ActiveCooldown;
    public Queue<IEnumerator> CooldownQueue = new Queue<IEnumerator>();
    
    public virtual void Awake(){
        Player = this.gameObject.GetComponent<Transform>();
        Charges = MaxCharges;
    }
    
    public virtual void Start(){
        Player = GetComponent<Transform>();
        Charges = MaxCharges;
    }
    public virtual void Run(){
        if(CooldownTime > 0.0f || MaxCharges > 0){
            CooldownQueue.Enqueue(ActivateCooldown2(CooldownTime));
            Charges--;
            ChargeChangeEnter();
        }
    }

    public virtual void Update(){
        if(Input.GetKeyDown(activasionKey)){
            Debug.Log("Check Ability");
            if(Charges == 0){
                return;
            }
            Run();
        }
        QueueCooldowns();
    }

    IEnumerator ActivateCooldown(float waitTime){
        CooldownRunning = true;
        yield return new WaitForSeconds(waitTime);
        Charges++;
        CooldownRunning = false;
    }

    public float CurrentCooldownTime;
    IEnumerator ActivateCooldown2(float waitTime){
        CooldownRunning = true;
        CurrentCooldownTime = waitTime;
        ChangeCooldownTimeRemaingEnter();
        while(CurrentCooldownTime > 0.0f){
            CurrentCooldownTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Charges++;
        ChargeChangeEnter();
        CooldownRunning = false;
        ChangeCooldownTimeRemaingEnter();
    }

    public event Action OnChangeCooldownTimeRemaingEnter;
    public void ChangeCooldownTimeRemaingEnter(){
        if(OnChangeCooldownTimeRemaingEnter != null){
            OnChangeCooldownTimeRemaingEnter();
        }
    }

    public event Action OnChargeChangeEnter;
    public void ChargeChangeEnter(){
        if(OnChargeChangeEnter != null){
            Debug.Log("Check event");
            OnChargeChangeEnter();
        }
    }

    public void CancelCooldowns(){
        int i;
        int size = CooldownQueue.Count;
        for(i = 0; i < size; i++){
            CooldownQueue.Dequeue();
        }
        if(CooldownRunning){
            StopCoroutine(ActiveCooldown);
        }
        Charges = MaxCharges;
    }

    public void IncreaseMaxCharges(){
        MaxCharges++;
        CooldownQueue.Enqueue(ActivateCooldown(CooldownTime));
    }

    private void QueueCooldowns(){
        if(CooldownRunning || CooldownQueue.Count == 0){
            return;
        }
        ActiveCooldown = StartCoroutine(CooldownQueue.Dequeue());
    }

    public enum AbilityType{
        Primary,
        Secondary,
        Mobility,
        Tech,
        None
    }
}
