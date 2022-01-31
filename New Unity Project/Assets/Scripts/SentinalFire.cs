using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinalFire : MonoBehaviour
{
    [SerializeField] private List<HealthController> targets = new List<HealthController>();
    private CircleCollider2D ImpactCollider;
    private float Damage;
    private void Awake(){
        ImpactCollider = GetComponent<CircleCollider2D>();
        ImpactCollider.isTrigger = true;
    }
    public void Activate(float damage, float tickRate, float impactRadius){
        this.Damage = damage;
        ImpactCollider.radius = impactRadius;
        InvokeRepeating("DoDamage", 0.0f, tickRate);
    }

    public void Deactivate(){
        targets.Clear();
        CancelInvoke();
    }

    public void SetPosition(Vector2 position){
        ImpactCollider.transform.position = position;
    }

    private void DoDamage(){
        int numTargets = targets.Count;
        int i;
        for(i = 0; i < numTargets; i++){
            targets[i].DoDamage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag.Equals("Player")){
            HealthController target = other.GetComponent<HealthController>();
            if(!targets.Contains(target)){
                targets.Add(other.GetComponent<HealthController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag.Equals("Player")){
            targets.Remove(other.GetComponent<HealthController>());
        }
    }


}
