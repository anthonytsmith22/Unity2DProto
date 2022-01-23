using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInfo : MonoBehaviour
{
    #region Singleton
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of PlayerInfo found!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private int EnergyAmount = 0;

    private  TextMeshProUGUI EnergyCounterText;

    private void Start(){
        EnergyCounterText = GameObject.Find("UI").transform.Find("Canvas")
        .transform.Find("EnergyCounter").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        UpdateEnergyText();
    }

    public void AddEnergy(int amount){
        EnergyAmount += amount;
        UpdateEnergyText();
    }

    private void UpdateEnergyText(){
        EnergyCounterText.text = EnergyAmount.ToString();
    }

}
