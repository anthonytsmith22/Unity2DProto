using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject EnemyPrefab;
    private BoxCollider2D SpawnerCollider;
    [SerializeField] private float spawnTime = 5f;

    private void Awake(){
        SpawnerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start(){
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Spawn(){
        GameObject enemy = Instantiate(EnemyPrefab, SpawnPoint.transform.position, EnemyPrefab.transform.rotation);
        BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(enemyCollider, SpawnerCollider);
    }

}
