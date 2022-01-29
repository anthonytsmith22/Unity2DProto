using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinalCombat : EnemyCombat
{
    public float damagePerTick = 50f;
    public float tickTime = 0.5f;
    public float chargeTime = 1.5f;
    public float FireTime = 3f;
    public LineRenderer FireRenderer;
    public LineRenderer ChargeRenderer;

    public override void Awake(){
        FireRenderer = Instantiate(FireRenderer);
        ChargeRenderer = Instantiate(ChargeRenderer);
        ChargeRenderer.enabled = false;
        FireRenderer.enabled = false;
    }
    public override void Start()
    {
        attackRadius = 7f;
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player")){
            Engaged = true;
            if(CancelEngagementRunning){
                StopCoroutine(CancelEngagement);
                CancelEngagementRunning = false;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player")){
            CancelEngagement = StartCoroutine(Disengage(EngagedExitTime));
        }
    }

    public Vector2 targetLocation;
    public Vector2 activeFireLocation;
    private void Shoot(){
        Debug.Log("Shooting");
        RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, activeFireLocation);
        if(hit.transform.tag.Equals("Player")){
            HealthController player = hit.transform.GetComponent<HealthController>();
            player.DoDamage(damagePerTick);
        }
        ChargeRenderer.enabled = true;
    }

    private void Update(){
        if(Engaged && !isFireRunning){
            StartCoroutine(Fire());
        }
    }

    private bool isFireRunning = false;
    IEnumerator Fire(){
        isFireRunning = true;
        targetLocation = Target.position;
        ChargeRenderer.enabled = true;
        float currentChargeTime = chargeTime;

        while(currentChargeTime > 0.0f){
            targetLocation = Target.position;
            activeFireLocation = Vector2.Lerp(activeFireLocation, targetLocation, Time.deltaTime*10);
            ChargeRenderer.SetPosition(0, FirePoint.position);
            ChargeRenderer.SetPosition(1, activeFireLocation);
            currentChargeTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        ChargeRenderer.enabled = false;
        FireRenderer.enabled = true;
        float currentFireTime = FireTime;
        InvokeRepeating("Shoot", 0.0f, tickTime);
        while(currentFireTime > 0.0f){
            ChargeRenderer.enabled = false;
            targetLocation = Target.position;
            activeFireLocation = Vector2.Lerp(activeFireLocation, targetLocation, Time.deltaTime*10);
            FireRenderer.SetPosition(0, FirePoint.position);
            FireRenderer.SetPosition(1, activeFireLocation);
            currentFireTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CancelInvoke("Shoot");
        FireRenderer.enabled = false;
        isFireRunning = false;
    }

    private void OnDestroy(){
        Destroy(ChargeRenderer);
        Destroy(FireRenderer);

    }
    
}
