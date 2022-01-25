using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private Transform Player;
    public int Charges;
    public int MaxCharges = -1;
    public float CooldownTime = 0f;
    public bool CooldownRunning = false;
    public KeyCode activasionKey;
    private Coroutine ActiveCooldown;
    private Queue<IEnumerator> CooldownQueue = new Queue<IEnumerator>();
    public virtual void Start(){
        Player = GetComponent<Transform>();
        Charges = MaxCharges;
    }
    public virtual void Run(){
        if(CooldownTime > 0.0f && !CooldownRunning){
            CooldownQueue.Enqueue(ActivateCooldown(CooldownTime));
        }

    }

    public virtual void Update(){
        if(Input.GetKeyDown(activasionKey)){
            if(CooldownRunning || Charges == 0){
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

    }

    private void QueueCooldowns(){
        if(CooldownRunning){
            return;
        }
        ActiveCooldown = StartCoroutine(CooldownQueue.Dequeue());
    }
}
