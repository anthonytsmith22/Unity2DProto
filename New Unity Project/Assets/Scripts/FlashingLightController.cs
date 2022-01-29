using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class FlashingLightController : MonoBehaviour
{
    public Light2D SpotLight;
    public float MinIntensity =  0.5f;
    public float MaxIntensity = 2.0f;
    public float FlashInterval = 2f;
    public float CurrentIntensity;
    public float IntensityChangeRate;

    private void Awake(){
        SpotLight = GetComponent<Light2D>();
    }

    private void Start(){
        CurrentIntensity = MinIntensity;
        IntensityChangeRate = (MaxIntensity - MinIntensity) / FlashInterval;
        SpotLight.intensity = CurrentIntensity;
        StartCoroutine(Flash());
    }

    private IEnumerator Flash(){
        while(CurrentIntensity < MaxIntensity){
            CurrentIntensity += IntensityChangeRate * Time.deltaTime;
            SpotLight.intensity = CurrentIntensity;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(Fade());
    }

    private IEnumerator Fade(){
        while(CurrentIntensity > MinIntensity){
            CurrentIntensity -= IntensityChangeRate * Time.deltaTime;
            SpotLight.intensity = CurrentIntensity;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(Flash());
    }

}
