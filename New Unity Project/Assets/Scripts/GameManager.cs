using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;

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
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Start(){
        if(PlayerCamera == null){
            PlayerCamera = GameObject.Find("PlayerCamera");
        }
        if(MainCamera == null){
            MainCamera = Camera.main;
        }
        cmCamera = PlayerCamera.GetComponent<CinemachineVirtualCamera>();
        EnableVSync();
        CapFPS();
        DisableMouseCursor();
        SetCameraOrthographicSize(zoom);
        //SetDefaultCMCameraConfiner();
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

    private void FixedUpdate(){
        //AstarPath.active.Scan();
        
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
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private Camera MainCamera;
    private CinemachineVirtualCamera cmCamera; 
    public void SetCameraOrthographicSize(float size){
        Debug.Log("Check Ortho");
        // Camera camera = Camera.main;
        // CinemachineBrain brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        // CinemachineVirtualCamera vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if(cmCamera != null){
            cmCamera.m_Lens.OrthographicSize = size;
            Debug.Log("Check 2");
        }
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetDefaultCMCameraConfiner(){
        PolygonCollider2D startRoomConfiner = GameObject.Find("StartRoomConfiner").GetComponent<PolygonCollider2D>();
        CameraConfinerController roomConfinerController = startRoomConfiner.GetComponent<CameraConfinerController>();
        CinemachineConfiner cinemachineConfiner = GameObject.Find("PlayerCamera").GetComponent<CinemachineConfiner>();
        cinemachineConfiner.m_BoundingShape2D = startRoomConfiner;
        SetCameraOrthographicSize(roomConfinerController.OrthoCameraSize);
    }
}
