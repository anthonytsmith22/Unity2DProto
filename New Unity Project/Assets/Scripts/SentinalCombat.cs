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
    
    [SerializeField] private GameObject AttackPoint;
    private SentinalFire FireController;
    [SerializeField] private float ImpactRadius = 0.2f;

    public override void Awake(){
        FireController = AttackPoint.GetComponent<SentinalFire>();
        FireRenderer = Instantiate(FireRenderer, FirePoint.position, Quaternion.identity, transform);
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
            // RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, activeFireLocation);
            // ChargeRenderer.SetPosition(0, FirePoint.position);
            // if(hit.transform.tag.Equals("CollisionEnvironment")){
            //     ChargeRenderer.SetPosition(1, hit.point);
            // }else{
            //     ChargeRenderer.SetPosition(1, activeFireLocation);
            // }
            ChargeRenderer.SetPosition(0, FirePoint.position);
            ChargeRenderer.SetPosition(1, activeFireLocation);
            currentChargeTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        ChargeRenderer.enabled = false;
        FireRenderer.enabled = true;
        float currentFireTime = FireTime;
        FireController.Activate(damagePerTick, tickTime, ImpactRadius);
        while(currentFireTime > 0.0f){
            ChargeRenderer.enabled = false;
            targetLocation = Target.position;
            activeFireLocation = Vector2.Lerp(activeFireLocation, targetLocation, Time.deltaTime);
            // RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, activeFireLocation);
            // Debug.Log(hit.);
            // FireRenderer.SetPosition(0, FirePoint.position);
            // if(hit.transform.tag.Equals("CollisionEnvironment")){
            //     Debug.Log("check hit");
            //     FireRenderer.SetPosition(1, hit.transform.position);
            // }else{
            //     FireRenderer.SetPosition(1, activeFireLocation);
            // }
            FireRenderer.SetPosition(0, FirePoint.position);
            FireRenderer.SetPosition(1, activeFireLocation);
            FireController.SetPosition(activeFireLocation);
            currentFireTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        FireController.Deactivate();
        FireRenderer.enabled = false;
        isFireRunning = false;
    }

    private void OnDestroy(){
        Destroy(ChargeRenderer.gameObject);
        Destroy(FireRenderer.gameObject);

    }
    
}
