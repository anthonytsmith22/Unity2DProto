using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpawners : MonoBehaviour
{
    [SerializeField] private List<GameObject> Spawners = new List<GameObject>();

    [SerializeField] private bool activate = false;
    
    private void Start(){
        if(activate){
            ToggleSpawners();
        }
    }

    private void ToggleSpawners(){
        int spawners = Spawners.Count;
        if(spawners > 0){
            int i;
            for(i = 0; i < spawners; i++){
                Spawners[i].GetComponent<EnemySpawner>().enabled = false;
            }
        }
    }
}
