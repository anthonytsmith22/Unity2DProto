using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of GameManager found!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private void Start(){
        EnableVSync();
        CapFPS();
        DisableMouseCursor();
        SetCameraOrthographicSize();
    }
    [SerializeField] private int FPS;
    private void CapFPS(){
        // If VSync is disabled we can set a FPS cap
        if(vsyncState == VSyncState.disabled){
            if(FPS == 0){
            FPS = 60;
            }
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = FPS;
        }
    }

    [SerializeField] private VSyncState vsyncState = VSyncState.disabled;
    public enum VSyncState{
        disabled,
        enabled,
        half
    }
    private void EnableVSync(){
        switch(vsyncState){
            case VSyncState.disabled:
                QualitySettings.vSyncCount = 0;
                break;
            case VSyncState.enabled:
                QualitySettings.vSyncCount = 1;
                break;
            case VSyncState.half:
                QualitySettings.vSyncCount = 2;
                break;
        }
    }

    private void DisableMouseCursor(){
        Cursor.visible = false;
    }

    [SerializeField] private float zoom = 5;
    private void SetCameraOrthographicSize(){
        Camera camera = Camera.main;
        CinemachineBrain brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        CinemachineVirtualCamera vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if(vcam != null){
            vcam.m_Lens.OrthographicSize = zoom;
        }
    }
}
