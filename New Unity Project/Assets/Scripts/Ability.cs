using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            CooldownQueue.Enqueue(ActivateCooldown(CooldownTime));
            Charges--;
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
